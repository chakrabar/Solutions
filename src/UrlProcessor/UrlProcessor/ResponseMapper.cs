﻿using SharedModels;
using ViewModels;

namespace UrlProcessor
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
            result.RequestStatus = queuingStatus == QueuingStatus.FAILED ? RequestStatus.Failed : RequestStatus.Successful;
            return result;
        }
    }
}
