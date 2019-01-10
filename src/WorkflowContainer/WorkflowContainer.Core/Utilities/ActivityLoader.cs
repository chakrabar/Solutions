using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xaml;

namespace WorkflowContainer.Core.Utilities
{
    public static class ActivityLoader
    {
        public static IDictionary<WorkflowIdentity, Activity> FetchAllActivitiesFromPath(string activitiesDirectory)
        {
            var activityIndex = new Dictionary<WorkflowIdentity, Activity>();
            var assemblies = Directory.GetFiles(activitiesDirectory, "*.dll");
            foreach (var dllFile in assemblies)
            {
                var assembly = Assembly.LoadFile(Path.GetFullPath(dllFile));
                var version = assembly.GetName().Version;
                foreach (var type in assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType == typeof(Activity)))
                {
                    var activity = (Activity)assembly.CreateInstance(type.FullName);
                    activityIndex.Add(new WorkflowIdentity(type.Name, version, new FileInfo(dllFile).Name), activity);
                }
            }
            return activityIndex;
        }

        public static Activity LoadActivityFromFile(string activityXamlPath, string activityAssemblyPath = null)
        {
            XamlReader xamlReader;
            if (!string.IsNullOrWhiteSpace(activityAssemblyPath))
            {
                Assembly actAssembly = Assembly.LoadFile(@"Workflows.dll");
                XamlXmlReaderSettings settings = new XamlXmlReaderSettings
                {
                    LocalAssembly = actAssembly
                };
                xamlReader = new XamlXmlReader(activityXamlPath, settings);
            }
            else
            {
                xamlReader = new XamlXmlReader(activityXamlPath);
            }
            Activity activity = ActivityXamlServices.Load(xamlReader);
            return activity;
        }

        public static Activity LoadActivityFromXaml(string activityXaml)
        {
            return ActivityXamlServices.Load(new StringReader(activityXaml));
        }
    }
}
