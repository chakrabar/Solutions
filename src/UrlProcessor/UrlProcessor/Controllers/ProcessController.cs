using AppContracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ViewModels;

namespace UrlProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IResourceProcessor _resourceProcessor;

        public ProcessController(IResourceProcessor resourceProcessor)
        {
            _resourceProcessor = resourceProcessor;
        }

        [HttpPost]
        public QueuingResponse PostUrls([FromBody]IEnumerable<string> urlsToProcess)
        {
            var (batchId, queueStatus) = _resourceProcessor.AddResourceBatchToQueue(urlsToProcess);
            return ResponseMapper.GetQueuingResponse(batchId, queueStatus);
        }
    }
}