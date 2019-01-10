using System;
using System.Collections.Generic;
using System.Web.Http;
using WorkflowContainer.API.Helpers;
using WorkflowContainer.API.ViewModels;

namespace WorkflowContainer.API.Controllers
{
    public class WorkflowController : ApiController
    {
        [HttpPost]
        [Route("api/workflow/invoke/{workflowName}")]
        public WorkflowStatusViewModel Post(string workflowName, [FromBody]Dictionary<string, object> data)
        {
            var workflowId = WorkflowIndex.GetWorkflowIdentityByName(workflowName);
            var host = WorkflowHostFactory.Get();            
            var workflowResult = host.Start(WorkflowIndex.GetWorkflow, workflowId, data, LogWriter.Log);
            return new WorkflowStatusViewModel
            {
                Message = $"Workflow successfully invoked for name: {workflowName}",
                WorkflowResult = workflowResult
            };
        }

        [HttpPost]
        [Route("api/workflow/resume/{instanceId}/{bookmark}")]
        public WorkflowStatusViewModel Post(Guid instanceId, string bookmark, [FromBody]object data)
        {
            var host = WorkflowHostFactory.Get();
            var workflowResult = host.ResumeBookmark(instanceId, WorkflowIndex.GetWorkflow, bookmark, data, LogWriter.Log);
            return new WorkflowStatusViewModel
            {
                Message = $"Workflow successfully resumed for id: {instanceId}, from bookmark: {bookmark}",
                WorkflowResult = workflowResult
            };
        }

        //[HttpPost]
        //[Route("api/workflow/resume/{instanceId}/{bookmark}/{approval}")]
        //public WorkflowStatusViewModel Post(Guid instanceId, string bookmark, string approval)
        //{
        //    var host = WorkflowHostFactory.Get();
        //    var workflowResult = host.ResumeBookmark(instanceId, WorkflowIndex.GetWorkflow, bookmark, approval, LogWriter.Log);
        //    return new WorkflowStatusViewModel
        //    {
        //        Message = $"Workflow successfully resumed for id: {instanceId}, from bookmark: {bookmark}",
        //        WorkflowResult = workflowResult
        //    };
        //}
    }
}
