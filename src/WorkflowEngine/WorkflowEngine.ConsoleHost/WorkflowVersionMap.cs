using System;
using System.Activities;
using System.Collections.Generic;
using WorkflowEngine.Cmd.Workflows;

namespace WorkflowEngine.ConsoleHost
{
    public static class WorkflowVersionMap
    {
        static Dictionary<WorkflowIdentity, Activity> map;

        // Current version identities.  
        static public WorkflowIdentity StateMachineNumberGuessIdentity;
        static public WorkflowIdentity FlowchartNumberGuessIdentity;
        static public WorkflowIdentity SequentialNumberGuessIdentity;

        static WorkflowVersionMap()
        {
            map = new Dictionary<WorkflowIdentity, Activity>();

            // Add the current workflow version identities.  
            StateMachineNumberGuessIdentity = new WorkflowIdentity
            {
                Name = "StateMachineNumberGuessWorkflow",
                Version = new Version(1, 0, 0, 0)
            };

            FlowchartNumberGuessIdentity = new WorkflowIdentity
            {
                Name = "FlowchartNumberGuessWorkflow",
                Version = new Version(1, 0, 0, 0)
            };

            SequentialNumberGuessIdentity = new WorkflowIdentity
            {
                Name = "SequentialNumberGuessWorkflow",
                Version = new Version(1, 0, 0, 0)
            };

            //I do not have these workflows <<<<<
            //map.Add(StateMachineNumberGuessIdentity, new StateMachineNumberGuessWorkflow());
            //map.Add(FlowchartNumberGuessIdentity, new FlowchartNumberGuessWorkflow());
            //map.Add(SequentialNumberGuessIdentity, new SequentialNumberGuessWorkflow());

            map.Add(StateMachineNumberGuessIdentity, new AddWorkflow());
            map.Add(FlowchartNumberGuessIdentity, new AddWorkflow()); //Should be different workflow
            map.Add(SequentialNumberGuessIdentity, new AddWorkflow()); //Should be different workflow
        }

        public static Activity GetWorkflowDefinition(WorkflowIdentity identity)
        {
            return map[identity];
        }

        public static string GetIdentityDescription(WorkflowIdentity identity)
        {
            return identity.ToString();
        }
    }
}
