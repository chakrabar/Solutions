using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ViewModels;

namespace UrlProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpPost]
        public QueuingResponse PostUrls([FromBody]IEnumerable<string> urlsToProcess)
        {
            return null;
        }
    }
}