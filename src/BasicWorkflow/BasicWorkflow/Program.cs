using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using ActivityLibrary1;
using System.ServiceModel.Syndication;

namespace BasicWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sequence = OutputToConsole("Hello", "WF");
            //var parallel = OutputToConsoleParallel("Yo", "Bengaluru");

            //WorkflowInvoker.Invoke(parallel);
            //WorkflowInvoker.Invoke(sequence);
            //WorkflowInvoker.Invoke(new Workflow1());
            //WorkflowInvoker.Invoke(new LeaveApprovalFlowchart());

            //var name = "AC";
            //var age = 30;
            //var workflowresult = WorkflowInvoker.Invoke(new WorkflowWithArguments(),
            //    new Dictionary<string, object>
            //    {
            //        { "name", name },
            //        { "age", age }
            //    });

            //var outPerson = workflowresult["person"] as Person;



            var activity = new GetFeed2
            {
                FeedUrl = "https://arghya.xyz/feed.xml"
            };

            var feedResult = WorkflowInvoker.Invoke<SyndicationFeed>
                (activity);


            //Activity workflow1 = new Workflow1();
            //WorkflowInvoker.Invoke(workflow1);

            System.Console.ReadLine();
        }

        private static Activity OutputToConsole(string v1, string v2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var s = new Sequence
            {
                Activities =
                {
                    new WriteLine { Text = v1 },
                    new WriteLine { Text = v2 }
                }
            };

            return s;
        }

        private static Activity OutputToConsoleParallel(string v1, string v2)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            var p = new Parallel
            {
                Branches =
                {
                    new WriteLine { Text = v1 },
                    new WriteLine { Text = v2 }
                }
            };

            return p;
        }
    }
}
