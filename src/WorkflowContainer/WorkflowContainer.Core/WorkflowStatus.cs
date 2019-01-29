namespace WorkflowContainer.Core
{
    public enum WorkflowStatus
    {
        Undefined = 0,
        Completed,
        Cancelled,
        Aborted,
        Errored,
        Persisted,
        Unloaded        
    }
}
