using System;
using System.Activities;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace WorkflowEngine.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new WorkflowHostForm());

            //Console.ReadLine();            
        }

        IDictionary<string, object> Host(Activity activity, IDictionary<string, object> inputs)
        {
            IDictionary<string, object> outputs = new Dictionary<string, object>();
            AutoResetEvent syncEvent = new AutoResetEvent(false); //initially closed
            AutoResetEvent idleEvent = new AutoResetEvent(false); //initially closed

            WorkflowApplication wfApp = new WorkflowApplication(activity, inputs)
            {
                Completed = delegate (WorkflowApplicationCompletedEventArgs e)
                {
                    outputs = e.Outputs;
                    syncEvent.Set();
                },
                Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
                {
                    Console.WriteLine(e.Reason);
                    syncEvent.Set();
                },
                OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
                {
                    Console.WriteLine(e.UnhandledException.ToString());
                    return UnhandledExceptionAction.Terminate;
                },
                Idle = delegate (WorkflowApplicationIdleEventArgs e)
                {
                    idleEvent.Set(); //when goes idle, let the process continue
                }
        };

            wfApp.Run();

            //syncEvent.WaitOne(); //wait for the application to COMPLETE / ABORT  ///// GO IDLE

            // Loop until the workflow for COMPLETE / ABORT / GO IDLE
            WaitHandle[] handles = new WaitHandle[] { syncEvent, idleEvent };
            while (WaitHandle.WaitAny(handles) != 0) //this is blocking & waiting, we need to fix this with persist & resume
            {
                // Gather the user input and resume the bookmark.
                bool validEntry = false;
                while (!validEntry)
                {
                    int Guess;
                    if (!Int32.TryParse(Console.ReadLine(), out Guess)) //this is custom logic, use a Func<data, bool instead>
                    {
                        Console.WriteLine("Please enter an integer.");
                    }
                    else
                    {
                        validEntry = true;
                        wfApp.ResumeBookmark("EnterGuess", Guess);
                    }
                }
            }

            // (assuming the workflow completed) return results
            return outputs;
        }
    }
}
