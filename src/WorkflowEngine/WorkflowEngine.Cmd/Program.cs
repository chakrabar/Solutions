using System;
using System.Collections.Generic;
using WorkflowEngine.Cmd.Workflows;
using WorkflowEngine.Core.Utilities;

namespace WorkflowEngine.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateXamlTests();
            ExecuteXamlTests();

            Console.ReadLine();
        }

        private static void ExecuteXamlTests()
        {
            var input = new Dictionary<string, object>
            {
                { "LeftVal", 10 },
                { "RightVal", 15 }
            };
            var result = WorkflowLoader.ExecuteXaml(@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\Workflow_Add.xaml", input);
            var sum = int.Parse(result["Sum"].ToString());

            var input2 = new Dictionary<string, object>
            {
                { "Operand1", 100 },
                { "Operand2", 105 }
            };
            var sum2 = WorkflowLoader.ExecuteXaml<int>(@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\WorkflowBuilder_Add.xaml", input2);
        }

        //Learning: There are some glitches while XAMLing pure code activities
        private static void CreateXamlTests()
        {
            var activity = new AddWorkflow();
            var xaml = activity.ToXamlString2();
            Console.WriteLine(xaml);

            activity.WriteToXamlFile2($@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\Workflow_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xaml");

            activity.WriteToXamlFile3($@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\Workflow_3_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xaml");

            activity.WriteToXamlFile4($@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\Workflow_4_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xaml");

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            var activityBuilder = SampleActivityBuilder.GetActivityBuilder();
            var xaml2 = activityBuilder.ToXamlString2();
            Console.WriteLine(xaml2);

            activityBuilder.WriteToXamlFile2($@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\WorkflowBuilder_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xaml");
        }
    }
}
