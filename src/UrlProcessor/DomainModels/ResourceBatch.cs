using System.Collections.Generic;

namespace DomainModels
{
    public class ResourceBatch
    {
        public int BatchId { get; set; }
        public IEnumerable<string> Resources { get; set; }
    }
}
