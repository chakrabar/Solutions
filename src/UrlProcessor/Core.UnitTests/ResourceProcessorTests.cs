using AppContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedModels;
using System.Collections.Generic;

namespace Core.UnitTests
{
    [TestClass]
    public class ResourceProcessorTests
    {
        Mock<IProcessQueue> _queueMock;
        Mock<IUrlBatchProcessor> _batchProcessorMock;
        ResourceProcessor _app;

        [TestInitialize]
        public void Setup()
        {
            _queueMock = new Mock<IProcessQueue>();
            _batchProcessorMock = new Mock<IUrlBatchProcessor>();
            _app = new ResourceProcessor(_queueMock.Object, _batchProcessorMock.Object);
        }

        [TestMethod]
        public void Should_add_to_queue_on_AddResourceBatchToQueue()
        {
            var urls = new[] { "url1", "url2" };
            _queueMock.Setup(m => m.Enqueue(It.IsAny<IEnumerable<string>>())).Returns(99);
            _batchProcessorMock.Setup(m => m.Trigger()).Verifiable();

            _app.AddResourceBatchToQueue(urls);

            _queueMock.Verify(m => m.Enqueue(urls), Times.Once());
            _batchProcessorMock.Verify(m => m.Trigger(), Times.Once());
        }

        [TestMethod]
        public void Should_handle_error_on_AddResourceBatchToQueue()
        {
            var urls = new[] { "url1", "url2" };
            _queueMock.Setup(m => m.Enqueue(It.IsAny<IEnumerable<string>>())).Throws<System.Exception>();

            var (BatchId, Status) = _app.AddResourceBatchToQueue(urls);

            Assert.AreEqual(BatchId, 0);
            Assert.AreEqual(Status, QueuingStatus.FAILED);
        }

        [TestMethod]
        public void Should_get_status_on_GetBatchStatus()
        {
            _queueMock.Setup(m => m.GetStatus(It.IsAny<int>())).Returns(QueuingStatus.QUEUED);

            var result = _app.GetBatchStatus(99);

            _queueMock.Verify(m => m.GetStatus(99), Times.Once());
            Assert.AreEqual(result, QueuingStatus.QUEUED);
        }

        [TestMethod]
        public void Should_handle_error_on_GetBatchStatus()
        {
            _queueMock.Setup(m => m.GetStatus(It.IsAny<int>())).Throws<System.Exception>();

            var result = _app.GetBatchStatus(99);

            Assert.AreEqual(result, QueuingStatus.FAILED);
        }
    }
}
