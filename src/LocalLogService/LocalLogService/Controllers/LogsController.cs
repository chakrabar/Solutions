using LocalLogService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        LogProcessor _logProcessor;

        public LogsController(LogProcessor logProcessor)
        {
            _logProcessor = logProcessor;
        }

        [HttpGet]
        public async Task<IEnumerable<Log>> GetLogsAsync()
        {
            var logs = await _logProcessor.GetLogsAsync();
            return logs;
        }

        [HttpGet("{id}")]
        public async Task<Log> GetLogByIdAsync(int id)
        {
            var log = await _logProcessor.GetLogAsync(id);
            return log;
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Log log)
        {
            var logId = await _logProcessor.WriteToLogAsync(log.Title, log.Message);
            return logId;
        }
    }
}