using System.Collections.Generic;

namespace Core
{
    public class ResourceProcessor
    {
        public int AddResourceBatchToQueue(IEnumerable<string> resources)
        {
            return ProcessQueue.Enqueue(resources);
        }
    }
}
