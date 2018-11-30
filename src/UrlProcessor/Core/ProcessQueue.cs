using AppContracts;
using DomainModels;
using SharedModels;
using System.Collections.Generic;

namespace Core
{
    //if we want to make the queue and status details durable and/or usable across multiple instances of application
    //we need to persist the queue and the dictionary to database. And every change must be updated to DB as well.
    public class ProcessQueue : IProcessQueue
    {
        private static readonly Dictionary<int, QueuingStatus> _statusLookup;
        private static readonly Queue<ResourceBatch> _requestQueue;
        private static int _queueSequence;
        private static readonly object _syncLock;

        static ProcessQueue()
        {
            _statusLookup = new Dictionary<int, QueuingStatus>();
            _requestQueue = new Queue<ResourceBatch>();
            _queueSequence = 0;
            _syncLock =  new object();
        }

        public int Enqueue(IEnumerable<string> resources)
        {
            lock(_syncLock)
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
        }

        public ResourceBatch Dequeue()
        {
            if (_requestQueue.Count == 0)
                return null;

            lock (_syncLock)
            {
                var data = _requestQueue.Dequeue();
                _statusLookup[data.BatchId] = QueuingStatus.INPROGRESS;
                return data;
            }            
        }

        public void MarkComplete(int id)
        {
            _statusLookup[id] = QueuingStatus.COMPLETED;
        }

        public void MarkFailed(int id)
        {
            _statusLookup[id] = QueuingStatus.FAILED;
        }

        public QueuingStatus GetStatus(int id)
        {
            return _statusLookup.ContainsKey(id)
                ? _statusLookup[id] : QueuingStatus.NOTFOUND;
        }
    }
}
