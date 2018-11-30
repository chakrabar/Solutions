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
                UrlBatchProcessor.Trigger(); //trigger processing of the batch
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
            try
            {
                return ProcessQueue.GetStatus(batchId);
            }
            catch (System.Exception)
            {
                //log error details
                return QueuingStatus.FAILED;
            }            
        }
    }
}
