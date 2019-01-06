using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.IO;

namespace WorkflowContainer.Core
{
    public class WorkflowHost
    {
        //string _connectionString; //= "Server=.\\SQLEXPRESS;Initial Catalog=WorkflowEngineStore;Integrated Security=SSPI";
        SqlWorkflowInstanceStore _store;

        public WorkflowHost(string storeConnectionString)
        {
            // Initialize the store and configure it so that it can be used for  
            // multiple WorkflowApplication instances.  
            _store = new SqlWorkflowInstanceStore(storeConnectionString);
            WorkflowApplication.CreateDefaultInstanceOwner(_store, null, WorkflowIdentityFilter.Any);
        }

        // >>>> START A WORKFLOW <<<<
        private void Start(Func<WorkflowIdentity, Activity> workflowMap,
            WorkflowIdentity workflowIdentity, IDictionary<string, object> inputs)
        {
            Activity workflow = workflowMap(workflowIdentity);
            WorkflowApplication wfApp = new WorkflowApplication(workflow, inputs, workflowIdentity);

            // Configure the instance store, extensions, and   
            // workflow lifecycle handlers.  
            ConfigureWorkflowApplication(wfApp);

            // Start the workflow.  
            wfApp.Run();
        }

        // >>>> RESUME A WORKFLOW <<<<
        private void ResumeBookmark(Guid workflowInstanceId,
            Func<WorkflowIdentity, Activity> workflowMap, string bookmarkName, object bookmarkResumeContext)
        {
            WorkflowApplicationInstance instance = WorkflowApplication.GetInstance(workflowInstanceId, _store);

            Activity workflow = workflowMap(instance.DefinitionIdentity);

            // Associate the WorkflowApplication with the correct definition (a name & version)
            WorkflowApplication wfApp = new WorkflowApplication(workflow, instance.DefinitionIdentity);

            // Configure the extensions and lifecycle handlers.  
            // Do this before the instance is loaded. Once the instance is  
            // loaded it is too late to add extensions.  
            ConfigureWorkflowApplication(wfApp);

            // Load the workflow.  
            wfApp.Load(instance); //takes a Guid InstanceId or WorkflowApplicationInstance instance from store

            // Resume the workflow.  
            wfApp.ResumeBookmark(bookmarkName, bookmarkResumeContext);
        }

        private void ConfigureWorkflowApplication(WorkflowApplication wfApp)
        {
            // Configure the persistence store.  
            wfApp.InstanceStore = _store;

            // Add a StringWriter to the extensions. This captures the output  
            // from the WriteLine activities so we can display it in the form.  
            StringWriter sw = new StringWriter();
            wfApp.Extensions.Add(sw);

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                if (e.CompletionState == ActivityInstanceState.Faulted)
                {
                    var message = string.Format("Workflow Terminated. Exception: {0}\r\n{1}",
                        e.TerminationException.GetType().FullName,
                        e.TerminationException.Message);
                    sw.WriteLine(message);
                }
                else if (e.CompletionState == ActivityInstanceState.Canceled)
                {
                    sw.WriteLine("Workflow Canceled.");
                }
                else
                {
                    sw.WriteLine("Workflow COMPLETED.");
                }
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                var message = string.Format("Workflow Aborted. Exception: {0}\r\n{1}",
                        e.Reason.GetType().FullName,
                        e.Reason.Message);
                sw.WriteLine(message);
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                var message = string.Format("Unhandled Exception: {0}\r\n{1}",
                        e.UnhandledException.GetType().FullName,
                        e.UnhandledException.Message);
                sw.WriteLine(message);
                return UnhandledExceptionAction.Terminate;
            };

            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                // Send the current WriteLine outputs to the status window.  
                var writers = e.GetInstanceExtensions<StringWriter>();
                foreach (var writer in writers)
                {
                    //UpdateStatus(writer.ToString());
                }
                return PersistableIdleAction.Unload;
            };
        }

        private void Unload(Guid workflowInstanceId) //that's weird!
        {
            WorkflowApplicationInstance instance = WorkflowApplication.GetInstance(workflowInstanceId, _store);

            // Unload the instance.  
            instance.Abandon();
        }

        private void Terminate(Guid workflowInstanceId, 
            Func<WorkflowIdentity, Activity> workflowMap, string terminationMessage)
        {
            WorkflowApplicationInstance instance = WorkflowApplication.GetInstance(workflowInstanceId, _store);

            // Use the persisted WorkflowIdentity to retrieve the correct workflow  
            // definition from the dictionary.  
            Activity wf = workflowMap(instance.DefinitionIdentity);

            // Associate the WorkflowApplication with the correct definition  
            WorkflowApplication wfApp = new WorkflowApplication(wf, instance.DefinitionIdentity);

            // Configure the extensions and lifecycle handlers  
            ConfigureWorkflowApplication(wfApp);

            // Load the workflow.  
            wfApp.Load(instance);

            // Terminate the workflow.  
            wfApp.Terminate(terminationMessage);
        }
    }
}
