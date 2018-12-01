using AppContracts;
using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UnitTests
{
    [TestClass]
    public class UrlBatchProcessorTests
    {
        Mock<IProcessQueue> _queueMock;
        Mock<IWebClient> _webClientMock;
        UrlBatchProcessor _app;

        [TestInitialize]
        public void Setup()
        {
            _queueMock = new Mock<IProcessQueue>();
            _webClientMock = new Mock<IWebClient>();
            _app = new UrlBatchProcessor(_queueMock.Object, _webClientMock.Object);
        }

        [TestMethod]
        public void Should_process_resources_on_Trigger()
        {
            var resourceBatch = new ResourceBatch { BatchId = 15, Resources = new[] { "url1", "url2" } };
            _queueMock.SetupSequence(m => m.Dequeue())
                .Returns(resourceBatch)
                .Returns((ResourceBatch)null);

            Task<string> stringTask = Task.Run(() => { Task.Delay(100); return "web data"; });
            _webClientMock.Setup(m => m.GetWebData(It.IsAny<string>())).Returns(stringTask);

            _app.Trigger();
            Thread.Sleep(1000); //wait for a second to allow the process to run

            _queueMock.Verify(m => m.Dequeue(), Times.Exactly(2));
            _webClientMock.Verify(m => m.GetWebData(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
