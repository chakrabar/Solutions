using System.Collections.Generic;
using DomainModels;
using SharedModels;

namespace AppContracts
{
    public interface IProcessQueue
    {
        ResourceBatch Dequeue();
        int Enqueue(IEnumerable<string> resources);
        QueuingStatus GetStatus(int id);
        void MarkComplete(int id);
        void MarkFailed(int id);
    }
}