using System.Configuration;

namespace WorkflowContainer.API.Helpers
{
    internal static class Settings
    {
        const string _instanceStoreConnectionStringName = "InstanceStore";
        const string _activitiesDirectoryName = "ActivitiesDirectory";

        internal static string GetActivitiesDirectory()
        {
            return ConfigurationManager
                .AppSettings[_activitiesDirectoryName];
        }

        internal static string GetInstanceStoreConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings[_instanceStoreConnectionStringName]
                .ConnectionString;
        }
    }
}