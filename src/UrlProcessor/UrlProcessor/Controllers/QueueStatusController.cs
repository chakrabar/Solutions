using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace UrlProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueStatusController : ControllerBase
    {
        [HttpGet("{id}")]
        public QueuingStatus Get(int id)
        {
            return QueuingStatus.QUEUED;
        }
    }
}