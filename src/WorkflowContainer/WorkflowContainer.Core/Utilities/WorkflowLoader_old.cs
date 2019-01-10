using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;

namespace WorkflowContainer.Core.Utilities
{
    public static class WorkflowLoader_old
    {
        public static IDictionary<string, object> ExecuteXaml(string xamlFilepath, IDictionary<string, object> data)
        {
            string workflowXaml = File.ReadAllText(xamlFilepath);
            DynamicActivity workflow = Load(workflowXaml);
            return WorkflowInvoker.Invoke(workflow, data);
        }

        public static T ExecuteXaml<T>(string xamlFilepath, IDictionary<string, object> data)
        {
            string workflowXaml = File.ReadAllText(xamlFilepath);
            DynamicActivity<T> workflow = Load<T>(workflowXaml);
            return WorkflowInvoker.Invoke(workflow, data);
        }

        public static IDictionary<string, object> LoadAndInvoke(string workflowXaml, IDictionary<string, object> data)
        {
            DynamicActivity workflow = Load(workflowXaml);
            return WorkflowInvoker.Invoke(workflow, data);
        }

        public static T LoadAndInvoke<T>(string workflowXaml, IDictionary<string, object> data)
        {
            DynamicActivity<T> workflow = Load<T>(workflowXaml);
            return WorkflowInvoker.Invoke(workflow, data);
        }

        public static DynamicActivity<T> Load<T>(string workflowXaml)
        {
            DynamicActivity<T> workflow = ActivityXamlServices.Load(new StringReader(workflowXaml)) as DynamicActivity<T>;
            return workflow;
        }

        public static DynamicActivity Load(string workflowXaml)
        {
            DynamicActivity workflow = ActivityXamlServices.Load(new StringReader(workflowXaml)) as DynamicActivity;
            return workflow;
        }

        public static IDictionary<string, object> Invoke(DynamicActivity workflow, IDictionary<string, object> data)
        {
            return WorkflowInvoker.Invoke(workflow, data);
        }

        public static T Invoke<T>(DynamicActivity<T> workflow, IDictionary<string, object> data)
        {
            return WorkflowInvoker.Invoke(workflow, data);
        }
    }
}
