using System;
using System.Activities;
using System.Collections.Generic;
using WorkflowContainer.Activities;

namespace WorkflowContainer.API.Helpers
{
    public static class WorkflowIndex
    {
        const string LongRunningRoutineName = "LongRunningRoutine";
        static Dictionary<WorkflowIdentity, Activity> _map;

        static WorkflowIndex()
        {
            _map = new Dictionary<WorkflowIdentity, Activity>();
            _map.Add(
                GetLongRunningRoutineIdentity(),
                LongRunningRoutine.Get());
        }

        public static WorkflowIdentity GetLongRunningRoutineIdentity()
        {
            return new WorkflowIdentity
            {
                Name = LongRunningRoutineName,
                Version = new Version(1, 0, 0, 0)
            };
        }

        public static Activity Get(WorkflowIdentity workflowIdentity)
        {
            if (_map.ContainsKey(workflowIdentity))
                return _map[workflowIdentity];

            throw new ArgumentException(workflowIdentity.Name);
        }
    }
}