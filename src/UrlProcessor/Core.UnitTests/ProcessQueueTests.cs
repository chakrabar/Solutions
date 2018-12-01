using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedModels;

namespace Core.UnitTests
{
    [TestClass]
    public class ProcessQueueTests
    {
        private ProcessQueue _queue;
        private int _batchId;

        [TestInitialize]
        public void Setup()
        {
            _queue = new ProcessQueue();
            _batchId = _queue.Enqueue(new[] { "url1", "url2" });
        }

        [TestMethod]
        public void Should_add_to_queue_on_Enqueue()
        {
            var urls = new[] { "url1", "url2" };

            var result = _queue.Enqueue(urls);
            var status = _queue.GetStatus(result);

            Assert.IsTrue(result > 0);
            Assert.AreEqual(status, QueuingStatus.QUEUED);
        }

        [TestMethod]
        public void Should_get_item_on_Dequeue()
        {
            var result = _queue.Dequeue();
            var status = _queue.GetStatus(result.BatchId);

            Assert.IsNotNull(result);
            Assert.AreEqual(status, QueuingStatus.INPROGRESS);
        }
        
        [TestMethod]
        public void Should_MarkComplete()
        {
            _queue.MarkComplete(_batchId);
            var status = _queue.GetStatus(_batchId);

            Assert.AreEqual(status, QueuingStatus.COMPLETED);
        }

        [TestMethod]
        public void Should_MarkFailed()
        {
            _queue.MarkFailed(_batchId);
            var status = _queue.GetStatus(_batchId);

            Assert.AreEqual(status, QueuingStatus.FAILED);
        }

        [TestMethod]
        public void Should_get_correct_status()
        {
            _queue.MarkFailed(_batchId);
            var status = _queue.GetStatus(_batchId);

            Assert.AreEqual(status, QueuingStatus.FAILED);
        }

        [TestMethod]
        public void Should_handle_invalid_id()
        {
            var status = _queue.GetStatus(int.MinValue);

            Assert.AreEqual(status, QueuingStatus.NOTFOUND);
        }
    }
}
