using AppContracts;
using SharedModels;
using System.Collections.Generic;

namespace Core
{
    public class ResourceProcessor : IResourceProcessor
    {
        public (int BatchId, QueuingStatus Status) AddResourceBatchToQueue(IEnumerable<string> resources)
        {
            try
            {
                var batchId = ProcessQueue.Enqueue(resources);
                return (batchId, QueuingStatus.QUEUED);
            }
            catch (System.Exception ex)
            {
                //log error details
                return (0, QueuingStatus.FAILED);
            }
        }

        public QueuingStatus GetBatchStatus(int batchId)
        {
            return ProcessQueue.GetStatus(batchId);
        }
    }
}
