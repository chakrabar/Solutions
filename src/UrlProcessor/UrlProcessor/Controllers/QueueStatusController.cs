using AppContracts;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace UrlProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueStatusController : ControllerBase
    {
        private readonly IResourceProcessor _resourceProcessor;

        public QueueStatusController(IResourceProcessor resourceProcessor)
        {
            _resourceProcessor = resourceProcessor;
        }

        [HttpGet("{id}")]
        public QueuingStatus Get(int id)
        {
            return _resourceProcessor.GetBatchStatus(id);
        }
    }
}