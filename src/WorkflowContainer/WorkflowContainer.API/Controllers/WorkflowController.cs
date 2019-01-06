using System;
using System.Web.Http;
using WorkflowContainer.API.Helpers;
using WorkflowContainer.Core;

namespace WorkflowContainer.API.Controllers
{
    public class WorkflowController : ApiController
    {
        [HttpPost]
        [Route("api/workflow/invoke/{workflowName}")]
        public void Post(string workflowName)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InstanceStore"].ConnectionString;
            var host = new WorkflowHost(connectionString);
            host.Start(WorkflowIndex.Get, WorkflowIndex.GetLongRunningRoutineIdentity(), null, LogWriter.Log);
        }

        [HttpPost]
        [Route("api/workflow/resume/{instanceId}/{bookmark}/{approval}")]
        public void Post(Guid instanceId, string bookmark, string approval)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InstanceStore"].ConnectionString;
            var host = new WorkflowHost(connectionString);
            host.ResumeBookmark(instanceId, WorkflowIndex.Get, bookmark, approval, LogWriter.Log);
        }
    }
}
