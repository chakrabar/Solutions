using System;
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
        public static IDictionary<WorkflowIdentity, Activity> FetchAllCodeActivitiesFromPath(string activitiesDirectory)
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

        public static IDictionary<WorkflowIdentity, Activity> FetchAllXamlActivitiesFromPath(string activitiesDirectory)
        {
            var activityIndex = new Dictionary<WorkflowIdentity, Activity>();
            var activityXamls = Directory.GetFiles(activitiesDirectory, "*.xaml");
            foreach (var xamlFile in activityXamls)
            {
                var version = new Version(1, 0, 0, 0); //TODO: Resolve to proper version
                var activityName = Path.GetFileNameWithoutExtension(xamlFile);
                var activity = LoadActivityFromFile(xamlFile);
                activityIndex.Add(new WorkflowIdentity(activityName, version, null), activity);
            }
            return activityIndex;
        }

        public static Activity LoadActivityFromFile(string activityXamlPath, string activityAssemblyPath = null)
        {
            XamlReader xamlReader;            
            if (!string.IsNullOrWhiteSpace(activityAssemblyPath))
            {
                Assembly actAssembly = Assembly.LoadFile(activityAssemblyPath);
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

            ActivityXamlServicesSettings activitySettings = new ActivityXamlServicesSettings
            {
                CompileExpressions = true //compile C# expressions in workflow
            };
            Activity activity = ActivityXamlServices.Load(xamlReader, activitySettings);

            return activity;
        }

        public static Activity LoadActivityFromXaml(string activityXaml)
        {
            return ActivityXamlServices.Load(new StringReader(activityXaml));
        }
    }
}
