using NuGet;
using System.Collections.Generic;

namespace WorkflowContainer.Core.NuGetManager
{
    public static class PackageRepoFactory
    {
        public static IPackageRepository Get(string packageSource)
        {
            return PackageRepositoryFactory
                .Default
                .CreateRepository(packageSource);
        }

        public static IPackageRepository Get(IEnumerable<string> packageSources)
        {
            return new AggregateRepository(PackageRepositoryFactory.Default, packageSources, false); //fail on incorrect source
        }
    }
}
