using AccessManagement.CommandStack.DataService;
using AccessManagement.External.Contracts;
using Microsoft.Practices.Unity;

namespace AccessManagement.Application.CommandStack
{
    public class AccessPointFacade : IAccessPointFacade
    {
        [Dependency]
        public IAccessPointService AccessPointService { get; set; }
        [Dependency]
        public IInsertDataService DataService { get; set; }

        public void SyncAccessPoints()
        {
            var accessPoints = AccessPointService.GetAccessPoints();
            var accessPointsInserted = DataService.InsertAccessPoints(accessPoints);
        }
    }
}
