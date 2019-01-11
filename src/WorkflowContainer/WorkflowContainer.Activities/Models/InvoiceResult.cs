namespace WorkflowContainer.Activities.Models
{
    public class InvoiceResult
    {
        public long InvoiceAmount { get; set; }
        public string Level1Approver { get; set; }
        public string Level2Approver { get; set; }
        public ApprovalData Approval1 { get; set; }
        public ApprovalData Approval2 { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAutoApproved { get; set; }
    }
}
