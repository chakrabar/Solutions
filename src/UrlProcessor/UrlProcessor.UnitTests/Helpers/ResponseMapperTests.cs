using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrlProcessor.Helpers;

namespace UrlProcessor.UnitTests.Helpers
{
    [TestClass]
    public class ResponseMapperTests
    {
        [TestMethod]
        public void Should_map_status_correctly()
        {
            var result = ResponseMapper.GetQueuingResponse(1, SharedModels.QueuingStatus.INPROGRESS);

            Assert.AreEqual(result.RequestStatus, ViewModels.RequestStatus.Successful);
        }

        [TestMethod]
        public void Should_map_error_status()
        {
            var result = ResponseMapper.GetQueuingResponse(1, SharedModels.QueuingStatus.FAILED);

            Assert.AreEqual(result.RequestStatus, ViewModels.RequestStatus.Failed);
        }
    }
}
