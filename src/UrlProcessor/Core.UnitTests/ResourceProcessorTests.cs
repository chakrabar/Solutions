using AppContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
    }
}
