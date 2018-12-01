using AppContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedModels;
using System.Collections.Generic;
using UrlProcessor.Controllers;

namespace UrlProcessor.UnitTests.Controllers
{
    [TestClass]
    public class ProcessControllerTests
    {
        Mock<IResourceProcessor> _resourceProcessorMock;
        ProcessController _controller;

        [TestInitialize]
        public void Setup()
        {
            _resourceProcessorMock = new Mock<IResourceProcessor>();
            _resourceProcessorMock.Setup(m => m.AddResourceBatchToQueue(It.IsAny<IEnumerable<string>>())).Returns((1, QueuingStatus.QUEUED));
            _controller = new ProcessController(_resourceProcessorMock.Object);
        }

        [TestMethod]
        public void Should_add_to_queue_on_post()
        {
            var urls = new[] { "url1", "url2" };

            _controller.PostUrls(urls);

            _resourceProcessorMock.Verify(m => m.AddResourceBatchToQueue(urls), Times.Once());
        }

        [TestMethod]
        public void Should_return_failure_on_post_error()
        {
            _resourceProcessorMock.Setup(m => m.AddResourceBatchToQueue(It.IsAny<IEnumerable<string>>())).Returns((1, QueuingStatus.FAILED));
            var urls = new[] { "url1", "url2" };

            var result = _controller.PostUrls(urls);

            Assert.AreEqual(result.RequestStatus, ViewModels.RequestStatus.Failed);
        }
    }
}
