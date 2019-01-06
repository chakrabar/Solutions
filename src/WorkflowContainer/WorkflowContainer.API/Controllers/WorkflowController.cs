using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WorkflowContainer.API.Controllers
{
    public class WorkflowController : ApiController
    {
        // GET: api/Workflow
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Workflow/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Workflow
        [HttpPost]
        [Route("api/workflow/invoke/{workflowName}")]
        public void Post(string workflowName)
        {
        }

        // PUT: api/Workflow/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Workflow/5
        public void Delete(int id)
        {
        }
    }
}
