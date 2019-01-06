﻿using System;
using System.Web.Http;
using WorkflowContainer.API.Helpers;

namespace WorkflowContainer.API.Controllers
{
    public class WorkflowController : ApiController
    {
        [HttpPost]
        [Route("api/workflow/invoke/{workflowName}")]
        public void Post(string workflowName)
        {
            var workflowId = WorkflowIndex.GetWorkflowIdentityByName(workflowName);
            var host = WorkflowHostFactory.Get();            
            host.Start(WorkflowIndex.GetWorkflow, workflowId, null, LogWriter.Log);
        }

        [HttpPost]
        [Route("api/workflow/resume/{instanceId}/{bookmark}/{approval}")]
        public void Post(Guid instanceId, string bookmark, string approval)
        {
            var host = WorkflowHostFactory.Get();
            host.ResumeBookmark(instanceId, WorkflowIndex.GetWorkflow, bookmark, approval, LogWriter.Log);
        }
    }
}
