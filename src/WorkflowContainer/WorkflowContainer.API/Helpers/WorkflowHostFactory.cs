using WorkflowContainer.Core;

namespace WorkflowContainer.API.Helpers
{
    internal class WorkflowHostFactory
    {
        internal static WorkflowHost Get()
        {
            var connectionString = Settings.GetInstanceStoreConnectionString();
            var host = new WorkflowHost(connectionString);
            return host;
        }
    }
}