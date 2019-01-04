using System;
using WorkflowEngine.Cmd.Workflows;
using WorkflowEngine.Utilities;

namespace WorkflowEngine.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            //var activity = new AddWorkflow();


            //var xaml = activity.ToXamlString();
            //Console.WriteLine(xaml);

            //activity.WriteToXamlFile($@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\AddWorkflow_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xaml");

            var activity = SampleActivityBuilder.GetActivityBuilder();

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            var xaml2 = activity.ToXamlString2();
            Console.WriteLine(xaml2);

            activity.WriteToXamlFile2($@"C:\ArghyaC\repos\Solutions\src\WorkflowEngine\TestData\AddWorkflow_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xaml");

            Console.ReadLine();
        }
    }
}
