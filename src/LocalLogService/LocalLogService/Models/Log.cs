using System;

namespace LocalLogService.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
