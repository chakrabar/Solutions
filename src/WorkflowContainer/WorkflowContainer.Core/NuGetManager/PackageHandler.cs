using NuGet;
using System.Collections.Generic;
using System.Linq;

namespace WorkflowContainer.Core.NuGetManager
{
    public class PackageHandler
    {
        private readonly IPackageRepository _packageRepository;

        public PackageHandler(string packagesSource)
        {
            _packageRepository = PackageRepoFactory.Get(packagesSource);
        }
        public PackageHandler(IEnumerable<string> packagesSources)
        {
            _packageRepository = PackageRepoFactory.Get(packagesSources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageId">EntityFramework</param>
        /// <param name="includePreRelease">true</param>
        /// <returns></returns>
        public List<IPackage> GetAllPackageVersions(string packageId, bool includePreRelease = false)
        {
            //Get the list of all NuGet packages with ID
            List<IPackage> packages = _packageRepository.FindPackagesById(packageId).ToList();

            //Filter the list of packages based on -if Release (Stable) versions
            if (!includePreRelease)
            {
                packages = packages
                    .Where(item => (item.IsReleaseVersion()))
                    .ToList();
            }

            return packages;
        }

        public IPackage GetLatestPackage(string packageId, bool includePreRelease = false)
        {
            var allPackageVersions = GetAllPackageVersions(packageId, includePreRelease);
            return allPackageVersions
                .SingleOrDefault(p => p.IsLatestVersion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageId">EntityFramework</param>
        /// <param name="version">5.0.0</param>
        /// <returns></returns>
        public IPackage GetPackage(string packageId, string version)
        {
            var allPackageVersions = GetAllPackageVersions(packageId, true);
            return allPackageVersions
                .SingleOrDefault(p => SemanticVersion.Parse(version).Equals(p.Version)); //TODO: check
        }

        public void InstallPackage(string packageID, string packageVersion, string installDirectory)
        {
            PackageManager packageManager = new PackageManager(_packageRepository, installDirectory);

            //Download and unzip the package
            packageManager.InstallPackage(packageID, SemanticVersion.Parse(packageVersion));
        }
    }
}
