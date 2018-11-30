using AppContracts;
using SharedModels;
using System.Collections.Generic;

namespace Core
{
    public class ResourceProcessor : IResourceProcessor
    {
        private readonly IProcessQueue _processQueue;
        private readonly IUrlBatchProcessor _urlBatchProcessor;

        public ResourceProcessor(IProcessQueue processQueue, IUrlBatchProcessor urlBatchProcessor)
        {
            _processQueue = processQueue;
            _urlBatchProcessor = urlBatchProcessor;
        }

        public (int BatchId, QueuingStatus Status) AddResourceBatchToQueue(IEnumerable<string> resources)
        {
            try
            {
                var batchId = _processQueue.Enqueue(resources);
                _urlBatchProcessor.Trigger(); //trigger processing of the batch
                return (batchId, QueuingStatus.QUEUED);
            }
            catch (System.Exception)
            {
                //log error details
                return (0, QueuingStatus.FAILED);
            }
        }

        public QueuingStatus GetBatchStatus(int batchId)
        {
            try
            {
                return _processQueue.GetStatus(batchId);
            }
            catch (System.Exception)
            {
                //log error details
                return QueuingStatus.FAILED;
            }            
        }
    }
}
