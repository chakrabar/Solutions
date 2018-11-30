using SharedModels;
using System.Collections.Generic;

namespace AppContracts
{
    public interface IResourceProcessor
    {
        (int BatchId, QueuingStatus Status) AddResourceBatchToQueue(IEnumerable<string> resources);
        QueuingStatus GetBatchStatus(int batchId);
    }
}
