using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.DurableInstancing;
using System.Threading;
using System.Threading.Tasks;
using WorkflowContainer.Core.Utilities;

namespace WorkflowContainer.Core
{
    public class WorkflowHost
    {
        readonly InstanceStore _store; //SqlWorkflowInstanceStore
        Guid _instanceId;
        IDictionary<string, object> _outputs = new Dictionary<string, object>();
        AutoResetEvent _syncEvent = new AutoResetEvent(false); //initially closed
        WorkflowStatus _workflowStatus = WorkflowStatus.Undefined;
        string _bookmarks = string.Empty;

        public WorkflowHost(string storeConnectionString)
        {
            // Initialize the store and configure it so that it can be used for  
            // multiple WorkflowApplication instances.  
            _store = new SqlWorkflowInstanceStore(storeConnectionString);
            WorkflowApplication.CreateDefaultInstanceOwner(_store, null, WorkflowIdentityFilter.Any);
            //_store = new RemoteInstanceStore(); //using WorkflowContainer.Infrastructure;
        }

        // >>>> START A WORKFLOW <<<<
        public WorkflowResult Start(Func<WorkflowIdentity, Activity> workflowMap, WorkflowIdentity workflowIdentity,
            IDictionary<string, object> inputs, Action<string> writelineListener = null)
        {
            if (inputs == null)
                inputs = new Dictionary<string, object>();

            Activity workflow = workflowMap(workflowIdentity);
            var castedData = WorkflowArgumentsHelper.DeserializeArguments(workflow as DynamicActivity, inputs);
            WorkflowApplication wfApp = new WorkflowApplication(workflow, castedData, workflowIdentity);

            // Configure the instance store, extensions, and   
            // workflow lifecycle handlers.  
            ConfigureWorkflowApplication(wfApp, writelineListener);

            // Start the workflow.
            wfApp.Run();

            return WaitAndReturnData();
        }

        // >>>> RESUME A WORKFLOW <<<<
        public WorkflowResult ResumeBookmark(Guid workflowInstanceId, Func<WorkflowIdentity, Activity> workflowMap,
            string bookmarkName, object bookmarkResumeContext, Action<string> writelineListener = null)
        {
            WorkflowApplicationInstance instance = WorkflowApplication.GetInstance(workflowInstanceId, _store);

            Activity workflow = workflowMap(instance.DefinitionIdentity);

            // Associate the WorkflowApplication with the correct definition (a name & version)
            WorkflowApplication wfApp = new WorkflowApplication(workflow, instance.DefinitionIdentity);

            // Configure the extensions and lifecycle handlers.  
            // Do this before the instance is loaded. Once the instance is  
            // loaded it is too late to add extensions.  
            ConfigureWorkflowApplication(wfApp, writelineListener);

            // Load the workflow.  
            wfApp.Load(instance); //takes a Guid InstanceId or WorkflowApplicationInstance instance from store

            // Resume the workflow.  
            wfApp.ResumeBookmark(bookmarkName, bookmarkResumeContext);

            return WaitAndReturnData();
        }

        private WorkflowResult WaitAndReturnData()
        {
            _syncEvent.WaitOne(TimeSpan.FromSeconds(300)); //wait for workflow to complete or go idle, with 10 second timeout

            return new WorkflowResult
            {
                InstanceId = _instanceId,
                Output = _outputs,
                Status = _workflowStatus,
                CurrentBookmarks = _bookmarks
            };
        }

        private void ConfigureWorkflowApplication(WorkflowApplication wfApp, Action<string> writelineListener = null)
        {
            // Configure the persistence store.  
            wfApp.InstanceStore = _store;

            // Add a StringWriter to the extensions. This captures the output  
            // from the WriteLine activities so we can display it in the form.  
            StringWriter sw = new StringWriter();
            wfApp.Extensions.Add(sw);

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                var message = string.Empty;
                if (e.CompletionState == ActivityInstanceState.Faulted)
                {
                    message = string.Format(wfApp.WorkflowDefinition?.DisplayName +
                                " Workflow Terminated. Exception: {0}\r\n{1}",
                                    e.TerminationException.GetType().FullName,
                                    GetExceptionMessage(e.TerminationException));                    
                    _workflowStatus = WorkflowStatus.Errored;
                }
                else if (e.CompletionState == ActivityInstanceState.Canceled)
                {
                    message = $"Workflow {wfApp.WorkflowDefinition?.DisplayName} CANCELLED.";
                    _workflowStatus = WorkflowStatus.Cancelled;
                }
                else
                {
                    message = $"Workflow {wfApp.WorkflowDefinition?.DisplayName} COMPLETED.";
                    _outputs = e.Outputs; //TODO: How to use it?
                    _workflowStatus = WorkflowStatus.Completed;
                }
                LogMessages(e, sw, message, writelineListener);
                _instanceId = e.InstanceId;
                //_syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                var message = string.Format(wfApp.WorkflowDefinition?.DisplayName +
                    " Workflow Aborted. Exception: {0}\r\n{1}",
                        e.Reason.GetType().FullName,
                        GetExceptionMessage(e.Reason));
                LogMessages(e, sw, message, writelineListener);
                _instanceId = e.InstanceId;
                _workflowStatus = WorkflowStatus.Aborted;
                _syncEvent.Set(); //abort does not trigger unload //TODO: verify
            };

            //on exception. Then it'll hit Completed()
            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                var message = string.Format(wfApp.WorkflowDefinition?.DisplayName +
                    " Unhandled Exception: {0}\r\n{1}",
                        e.UnhandledException.GetType().FullName,
                        GetExceptionMessage(e.UnhandledException));
                LogMessages(e, sw, message, writelineListener);
                _workflowStatus = WorkflowStatus.Errored;
                //_syncEvent.Set();
                return UnhandledExceptionAction.Terminate;
            };

            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                var message = $"Workflow {wfApp.WorkflowDefinition?.DisplayName} getting to IDLE.";
                LogMessages(e, sw, message, writelineListener, true); //clear messages as idle'll invoke Unloaded()                
                _bookmarks = string.Join(",", e.Bookmarks.Select(b => b.BookmarkName)); //TODO: check
                _instanceId = e.InstanceId;
                _workflowStatus = WorkflowStatus.Persisted;
                //_syncEvent.Set();
                return PersistableIdleAction.Unload;
            };

            //any time wf is unloaded, e.g. bookmark, completed, exception
            wfApp.Unloaded = delegate (WorkflowApplicationEventArgs e)
            {
                //_instanceId = e.InstanceId;
                //_workflowStatus = WorkflowStatus.Unloaded;
                var message = $"Workflow {wfApp.WorkflowDefinition?.DisplayName} UNLOADED.";
                LogMessages(e, sw, message, writelineListener);
                _syncEvent.Set();
            };

            string GetExceptionMessage(Exception exception)
            {
                var expMsg = "Exception:";
                var isInnerException = false;
                var exp = exception;
                while (exp != null)
                {
                    expMsg += $"{(isInnerException ? ". Inner Exp: " : " ")}{exp.ToString()}";
                    exp = exp.InnerException;
                    isInnerException = true;
                }
                return expMsg;
            }
        }

        private static void LogMessages(WorkflowApplicationEventArgs e, 
            StringWriter sw, string message, Action<string> writelineListener, bool clearMessages = false)
        {
            Task.Run(() =>
            {
                sw.WriteLine(message);
                // Send the current WriteLine outputs to the designated listner
                if (writelineListener != null)
                {
                    var writers = e.GetInstanceExtensions<StringWriter>();
                    foreach (var writer in writers)
                    {
                        writelineListener("Workflow Writeline Log : " + writer.ToString());
                    }
                }

                if (clearMessages)
                {
                    sw.GetStringBuilder().Clear(); //clear all data from the StringWriter
                }
            });            
        }

        #region Not-Used-Yet

        private void Unload(Guid workflowInstanceId) //that's weird!
        {
            WorkflowApplicationInstance instance = WorkflowApplication.GetInstance(workflowInstanceId, _store);

            // Unload the instance.  
            instance.Abandon();
        }

        private void Terminate(Guid workflowInstanceId, Func<WorkflowIdentity, Activity> workflowMap,
            string terminationMessage, Action<string> writelineListener = null)
        {
            WorkflowApplicationInstance instance = WorkflowApplication.GetInstance(workflowInstanceId, _store);

            // Use the persisted WorkflowIdentity to retrieve the correct workflow  
            // definition from the dictionary.  
            Activity wf = workflowMap(instance.DefinitionIdentity);

            // Associate the WorkflowApplication with the correct definition  
            WorkflowApplication wfApp = new WorkflowApplication(wf, instance.DefinitionIdentity);

            // Configure the extensions and lifecycle handlers  
            ConfigureWorkflowApplication(wfApp, writelineListener);

            // Load the workflow.  
            wfApp.Load(instance);

            // Terminate the workflow.  
            wfApp.Terminate(terminationMessage);
        }

        #endregion
    }
}
