using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using ActivityLibrary1;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualBasic.Activities;

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

            //var activity = new GetFeed2
            //{
            //    FeedUrl = "https://arghya.xyz/feed.xml"
            //};
            //var feedResult = WorkflowInvoker.Invoke<SyndicationFeed>
            //    (activity);

            //WorkflowInvoker.Invoke(GetMySequence());

            //PauseAndResumeNotCorrect();

            TestActivityFunc();


            //Activity workflow1 = new Workflow1();
            //WorkflowInvoker.Invoke(workflow1);

            System.Console.ReadLine();
        }

        private static void TestActivityFunc()
        {
            SendMailWithFunc sendMail = new SendMailWithFunc
            {
                FromAddress = "chakrabar@live.com",
                RecipientAddress = "ac@arghya.xyz",
                Subject = "How's your WF training going",
                CreateBodyText = new ActivityFunc<string> //the actual ActivityFunc
                {
                    Result = new DelegateOutArgument<string> { Name = "FuncResult" },
                    Handler = new Sequence
                    {
                        Activities =
                        {
                            new Assign<string>
                            {
                                To = new VisualBasicReference<string>("FuncResult"),
                                Value = $"This is a dynamically created email body. " +
                                $"This was generated on {DateTime.Now.ToLongDateString()} " +
                                $"at {DateTime.Now.ToLongTimeString()}"
                            }
                        }
                    }
                }
            };

            WorkflowInvoker.Invoke(sendMail);
        }

        private static void PauseAndResumeNotCorrect()
        {
            Variable<string> name = new Variable<string>();

            Activity wf = new Sequence
            {
                Variables = { name },
                Activities =
                 {
                     new WriteLine
                     {
                         Text = "Hello there!"
                     },
                     new WaitForData<string>
                     {
                         BookmarkName = "WaitForUserInputData",
                         Result = new OutArgument<string>(name)
                     },
                     new WriteLine
                     {
                         Text = new InArgument<string>((env) =>
                             ("Hello, " + name.Get(env)))
                     }
                 }
            };


            WorkflowApplication wfApp = new WorkflowApplication(wf);

            wfApp.Completed += (wce) =>
            {
                var response = "blah"; //(string)wce.Outputs["Result"];
                Console.Write($"Workflow completed, Result is {response}");
            };
            wfApp.Idle += (wie) =>
            {
                Console.Write("Workflow idle!");
            };

            wfApp.Run();

            Console.WriteLine("What's your name?");
            var input = Console.ReadLine();

            var result = wfApp.ResumeBookmark("WaitForUserInputData", input);
            Console.WriteLine($"Bookmark resume result: {result}, output data: {name.ToString()}");
        }

        private static Activity GetMySequence()
        {
            Variable<string> textValue = new Variable<string> { Default = "default value" };

            return new MySequence
            {
                Variables = { textValue },
                Activities =
                {
                    new WriteLine { Text = textValue },
                    new Assign<string> { To = textValue, Value = "assigned value" },
                    new WriteLine { Text = textValue }
                }
            };
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

            var p = new System.Activities.Statements.Parallel
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
