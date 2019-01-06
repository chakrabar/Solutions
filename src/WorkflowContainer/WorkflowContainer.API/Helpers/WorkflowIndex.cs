using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;

namespace WorkflowContainer.API.Helpers
{
    internal static class WorkflowIndex
    {
        
        static Dictionary<WorkflowIdentity, Activity> _map;

        static WorkflowIndex() //TODO: add other workflows
        {
            _map = new Dictionary<WorkflowIdentity, Activity>();
            foreach (var id in GetWorkflowIdentities())
            {
                _map.Add(
                    id,
                    WorkflowFactory.Get(id.Name) //TODO: use both NAME & VERSION
                );
            }
        }

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

        internal static WorkflowIdentity GetWorkflowIdentityByName(string workflowName) //TODO: this needs improvement
        {
            var availableIds = GetWorkflowIdentities();
            if (availableIds.Any(id => id.Name == workflowName))
                return availableIds.First(id => id.Name == workflowName); //TODO: use both NAME & VERSION
            throw new ArgumentException($"No identity found for : {workflowName}", "Workflow name");
        }

        internal static Activity GetWorkflow(WorkflowIdentity workflowIdentity)
        {
            if (_map.ContainsKey(workflowIdentity))
                return _map[workflowIdentity];

            throw new ArgumentException(workflowIdentity.Name);
        }
    }
}