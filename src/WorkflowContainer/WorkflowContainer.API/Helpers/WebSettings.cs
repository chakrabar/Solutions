using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace WorkflowContainer.API.Helpers
{
    internal static class WebSettings
    {
        const string _instanceStoreConnectionStringName = "InstanceStore";
        const string _activitiesDirectoryKey = "ActivitiesDirectory";
        const string _nugetSourceKeyPrefix = "NuGetSource";
        const string _nugetBinDirectoryKey = "NuGetBinaries";
        const string _machineNuGetStore = "MachineNuGetStore";

        internal static string GetActivitiesDirectory()
        {
            return ConfigurationManager
                .AppSettings[_activitiesDirectoryKey];
        }

        internal static string GetInstanceStoreConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings[_instanceStoreConnectionStringName]
                .ConnectionString;
        }

        internal static List<string> GetNugetPackageSources()
        {
            var nugetSourceKeys = ConfigurationManager
                .AppSettings
                .AllKeys
                .Where(key => key.StartsWith(_nugetSourceKeyPrefix));
            var nugetSources = new List<string>();
            foreach (var key in nugetSourceKeys)
            {
                nugetSources.Add(ConfigurationManager
                .AppSettings[key]);
            }
            return nugetSources;
        }

        internal static string GetNuGetBinariesDirectory()
        {
            return ConfigurationManager
                .AppSettings[_nugetBinDirectoryKey];
        }

        internal static string GetMachineNuGetStore()
        {
            return ConfigurationManager
                .AppSettings[_machineNuGetStore];
        }
    }
}