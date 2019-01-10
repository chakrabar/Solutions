using System;
using System.Activities;
//using WorkflowContainer.Activities;

namespace WorkflowContainer.API.Helpers
{
    internal static class WorkflowFactory
    {
        internal const string LongRunningRoutineName = "LongRunningRoutine";

        internal static Activity Get(string name)
        {
            switch(name)
            {
                case LongRunningRoutineName:
                    return null; // LongRunningRoutine.Get();
                default:
                    throw new ArgumentException($"No activity has been set for key : {name}", "Workflow name");
            }
        }
    }
}