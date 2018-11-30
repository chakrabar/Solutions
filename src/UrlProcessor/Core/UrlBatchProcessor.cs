using AppContracts;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core
{
    public class UrlBatchProcessor : IUrlBatchProcessor
    {
        private static volatile bool _isRunning = false;
        private static readonly object _syncLock = new object();
        private static readonly HttpClient _client = new HttpClient();
        private readonly IProcessQueue _processQueue;

        public UrlBatchProcessor(IProcessQueue processQueue)
        {
            _processQueue = processQueue;
        }

        public void Trigger()
        {
            if (!_isRunning)
                Task.Run(() => Process()); //start processing on a separate thread
        }

        private void Process()
        {
            if (_isRunning)
                return;

            lock(_syncLock)
            {
                _isRunning = true;

                var batchToProcess = _processQueue.Dequeue();
                while (batchToProcess != null)
                {
                    try
                    {
                        var processedBatchData = ProcessUrlBatch(batchToProcess);
                        //the actual processing is done
                        //do something with the processed data....
                        _processQueue.MarkComplete(batchToProcess.BatchId);
                    }
                    catch (Exception)
                    {
                        //log the error
                        _processQueue.MarkFailed(batchToProcess.BatchId);
                    }
                }

                _isRunning = false;
            }            
        }

        private IEnumerable<string> ProcessUrlBatch(ResourceBatch batch)
        {
            var urlData = new List<string>();
            foreach (var url in batch.Resources)
            {
                var dataTask = _client.GetStringAsync(url);
                //we could read the url ASYNChronously. But we already have a backing queue, so we'll process them in sequence
                urlData.Add(dataTask.Result);
            }
            return urlData;
        }
    }
}
