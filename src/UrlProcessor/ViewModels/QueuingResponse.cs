namespace ViewModels
{
    public class QueuingResponse
    {
        public int QueueId { get; set; }
        public QueuingStatus QueueStatus { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
}
