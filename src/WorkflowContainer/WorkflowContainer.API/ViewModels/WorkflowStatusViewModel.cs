using WorkflowContainer.Core;

namespace WorkflowContainer.API.ViewModels
{
    public class WorkflowStatusViewModel
    {
        public string Message { get; set; }
        public WorkflowResult WorkflowResult { get; set; }
    }
}