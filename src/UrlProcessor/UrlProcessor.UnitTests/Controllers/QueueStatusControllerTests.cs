using AppContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedModels;
using UrlProcessor.Controllers;

namespace UrlProcessor.UnitTests.Controllers
{
    [TestClass]
    public class QueueStatusControllerTests
    {
        Mock<IResourceProcessor> _resourceProcessorMock;
        QueueStatusController _controller;

        [TestInitialize]
        public void Setup()
        {
            _resourceProcessorMock = new Mock<IResourceProcessor>();
            _resourceProcessorMock.Setup(m => m.GetBatchStatus(It.IsAny<int>())).Returns(QueuingStatus.INPROGRESS);
            _controller = new QueueStatusController(_resourceProcessorMock.Object);
        }

        [TestMethod]
        public void Should_add_to_queue_on_post()
        {
            _controller.Get(100);

            _resourceProcessorMock.Verify(m => m.GetBatchStatus(100), Times.Once());
        }
    }
}
