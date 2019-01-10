using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorkflowContainer.Core.Utilities;

namespace WorkflowContainer.API.Helpers
{
    internal static class WorkflowIndex
    {        
        static IDictionary<WorkflowIdentity, Activity> _map;

        static WorkflowIndex() //TODO: add other workflows
        {
            LoadXamlActivitiesFromConfigPath();
        }

        internal static void LoadXamlActivitiesFromConfigPath()
        {
            var activitiesPath = Settings.GetActivitiesDirectory();
            _map = ActivityLoader.FetchAllXamlActivitiesFromPath(activitiesPath);
        }

        internal static WorkflowIdentity GetWorkflowIdentityByName(string workflowName) //TODO: this needs improvement
        {
            var availableWorkflows = _map.Where(dict => dict.Key.Name == workflowName);
            if (availableWorkflows.Any())
            {
                return availableWorkflows
                    .OrderBy(wf => wf.Key.Version)
                    .First()
                    .Key;
            }
            throw new ArgumentException($"No identity found for : {workflowName}", "Workflow name");
        }

        internal static Activity GetWorkflow(WorkflowIdentity workflowIdentity)
        {
            if (_map.ContainsKey(workflowIdentity))
                return _map[workflowIdentity];

            throw new ArgumentException(workflowIdentity.Name);
        }

        #region deprecated

        private static IEnumerable<WorkflowIdentity> GetWorkflowIdentities() //TODO: add other workflows
        {
            return new List<WorkflowIdentity>
            {
                new WorkflowIdentity
                {
                    Name = WorkflowFactory.LongRunningRoutineName,
                    Version = new Version(1, 0, 0, 0)
                }
            };
        }

        internal static WorkflowIdentity GetWorkflowIdentityByName_old(string workflowName) //TODO: this needs improvement
        {
            var availableIds = GetWorkflowIdentities();
            if (availableIds.Any(id => id.Name == workflowName))
                return availableIds.First(id => id.Name == workflowName); //TODO: use both NAME & VERSION
            throw new ArgumentException($"No identity found for : {workflowName}", "Workflow name");
        }

        private static void LoadSampleWorkflows()
        {
            _map = new Dictionary<WorkflowIdentity, Activity>();

            foreach (var id in GetWorkflowIdentities())
            {
                _map.Add(
                    id,
                    WorkflowFactory.Get(id.Name) //TODO: use both NAME & VERSION
                );
            }

            var assemblyPath = Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase);
            _map = ActivityLoader.FetchAllXamlActivitiesFromPath(assemblyPath);
        }
        #endregion
    }
}