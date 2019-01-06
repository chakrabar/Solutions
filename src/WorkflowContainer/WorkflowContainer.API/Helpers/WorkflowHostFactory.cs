using WorkflowContainer.Core;

namespace WorkflowContainer.API.Helpers
{
    internal class WorkflowHostFactory
    {
        const string _instanceStoreConnectionStringName = "InstanceStore";

        internal static WorkflowHost Get()
        {
            var connectionString = System.Configuration
                .ConfigurationManager
                .ConnectionStrings[_instanceStoreConnectionStringName]
                .ConnectionString;
            var host = new WorkflowHost(connectionString);
            return host;
        }
    }
}