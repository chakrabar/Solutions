using NuGet;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using WorkflowContainer.API.Helpers;
using WorkflowContainer.Core.NuGetManager;

namespace WorkflowContainer.API.Controllers
{
    [RoutePrefix("api/packages")]
    public class PackageController : ApiController
    {
        //const string _versionNumberRegex = @"\d\.\d\.\d(\d|\-[a-zA-Z0-9\-]{3,10})?";
        //[Route(@"api/packages/{packageId}/{version:regex(\d\.\d\.\d(\d|\-[a-zA-Z0-9\-]{3,10})?)}")]

        [HttpGet]
        [Route("getone/{packageId}/{version}")]
        public IPackage Get(string packageId, string version)
        {
            if (!string.IsNullOrWhiteSpace(version))
                version = version.Replace('_', '.');
            var packageSources = WebSettings.GetNugetPackageSources();
            var packageHandler = new PackageHandler(packageSources);
            return packageHandler.GetPackage(packageId, version);
        }

        [HttpGet]
        [Route("getall/{packageId}")]
        public IEnumerable<IPackage> Get(string packageId)
        {
            var packageSources = WebSettings.GetNugetPackageSources();
            var packageHandler = new PackageHandler(packageSources);
            return packageHandler.GetAllPackageVersions(packageId);
        }

        [HttpGet]
        [Route("getlatest/{packageId}/{includePrerelease:bool?}")]
        public IPackage Get(string packageId, bool includePrerelease = false)
        {
            var packageSources = WebSettings.GetNugetPackageSources();
            var packageHandler = new PackageHandler(packageSources);
            return packageHandler.GetLatestPackage(packageId, includePrerelease);
        }

        [HttpGet] //TODO: Remove or make POST
        [Route("install/{packageId}/{version}")]
        public async Task<string> Install(string packageId, string version)
        {
            if (!string.IsNullOrWhiteSpace(version))
                version = version.Replace('_', '.');
            var packageSources = WebSettings.GetNugetPackageSources();
            var packageHandler = new PackageHandler(packageSources);
            var packageTargetDir = WebSettings.GetNuGetBinariesDirectory();

            var sw = new Stopwatch();
            sw.Start();
            await Task.Run(() => packageHandler.InstallPackage(packageId, version, packageTargetDir));
            sw.Stop();

            return $"Package {packageId}.{version} was successfully installed on {packageTargetDir}, in {sw.Elapsed.ToString(@"hh\:mm\:ss\.fffff")} time";
        }
    }
}
