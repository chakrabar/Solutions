using DomainModels;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Core
{
    public static class UrlBatchProcessor
    {
        private static bool _isRunning = false;
        private static readonly object _syncLock = new object();
        private static readonly HttpClient _client = new HttpClient();

        public static void Trigger()
        {
            if (!_isRunning)
                Process();
        }

        private static void Process()
        {
            if (_isRunning)
                return;

            lock(_syncLock)
            {
                _isRunning = true;

                var batchToProcess = ProcessQueue.Dequeue();
                while (batchToProcess != null)
                {
                    try
                    {
                        var processedBatchData = ProcessUrlBatch(batchToProcess);
                        //the actual processing is done
                        //do something with the processed data....
                        ProcessQueue.MarkComplete(batchToProcess.BatchId);
                    }
                    catch (Exception)
                    {
                        //log the error
                        ProcessQueue.MarkFailed(batchToProcess.BatchId);
                    }
                }

                _isRunning = false;
            }            
        }

        private static IEnumerable<string> ProcessUrlBatch(ResourceBatch batch)
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
