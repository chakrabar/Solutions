using DomainModels;
using SharedModels;
using System.Collections.Generic;

namespace Core
{
    //if we want to make the queue and status details durable and/or usable across multiple instances of application
    //we need to persist the queue and the dictionary to database. And every change must be updated to DB as well.
    public static class ProcessQueue
    {
        private static readonly Dictionary<int, QueuingStatus> _statusLookup;
        private static readonly Queue<ResourceBatch> _requestQueue;
        private static int _queueSequence;

        static ProcessQueue()
        {
            _statusLookup = new Dictionary<int, QueuingStatus>();
            _requestQueue = new Queue<ResourceBatch>();
            _queueSequence = 0;
        }

        public static int Enqueue(IEnumerable<string> resources)
        {
            var id = ++_queueSequence;
            _requestQueue.Enqueue(new ResourceBatch
            {
                BatchId = id,
                Resources = resources
            });
            _statusLookup.Add(id, QueuingStatus.QUEUED);
            return id;
        }

        public static ResourceBatch Dequeue()
        {
            var data = _requestQueue.Dequeue();
            _statusLookup[data.BatchId] = QueuingStatus.INPROGRESS;
            return data;
        }

        public static void MarkComplete(int id)
        {
            _statusLookup[id] = QueuingStatus.COMPLETED;
        }

        public static void MarkFailed(int id)
        {
            _statusLookup[id] = QueuingStatus.FAILED;
        }

        public static QueuingStatus GetStatus(int id)
        {
            return _statusLookup.ContainsKey(id)
                ? _statusLookup[id] : QueuingStatus.NOTFOUND;
        }
    }
}
