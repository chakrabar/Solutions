using SharedModels;
using ViewModels;

namespace UrlProcessor.Helpers
{
    public class ResponseMapper
    {
        public static QueuingResponse GetQueuingResponse(int batchId, QueuingStatus queuingStatus)
        {
            var result = new QueuingResponse
            {
                QueueId = batchId,
                QueueStatus = queuingStatus
            };
            result.RequestStatus = (queuingStatus == QueuingStatus.FAILED || queuingStatus == QueuingStatus.NOTFOUND) 
                ? RequestStatus.Failed : RequestStatus.Successful;
            return result;
        }
    }
}
