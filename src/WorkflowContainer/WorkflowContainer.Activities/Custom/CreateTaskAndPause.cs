using System;
using System.Activities;
using System.Diagnostics;

namespace WorkflowContainer.Activities.Custom
{
    public class CreateTaskAndPause<TOut> : NativeActivity<TOut>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        [RequiredArgument]
        public InArgument<string> RequestMessage { get; set; }

        [RequiredArgument]
        public InArgument<string> AsigneeEmail { get; set; }

        public OutArgument<Guid> TaskId { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object data)
        {
            Trace.TraceInformation($"Resuming : {nameof(WorkflowRestorePoint<TOut>)}, from bookmark : {bookmark.Name}");
            TOut output = default(TOut);
            try
            {
                output = JsonUtils.Cast<TOut>(data);
                Trace.TraceInformation($"From : {nameof(WorkflowRestorePoint<TOut>)}. Bookmark resume data : {output.ToJson()}");
            }
            catch (InvalidCastException e)
            {
                Trace.TraceError($"From : {nameof(WorkflowRestorePoint<TOut>)}. Error casting data. Exception: {e.Message}");
                Trace.Flush();
                throw;
            }

            // When the Bookmark is resumed, assign its value to the Result argument
            Result.Set(context, output);

            Trace.Flush();
        }

        protected override void Execute(NativeActivityContext context)
        {
            Trace.TraceInformation($"Executing {nameof(CreateTask)}");
            var taskId = Guid.NewGuid();
            TaskId.Set(context, taskId);

            Trace.TraceInformation($"<<<< CREATED TASK >>>> Asignee: {AsigneeEmail.Get(context)}, " +
                $"RequestMessage: {RequestMessage.Get(context)}. " +
                $"Task ID: {taskId.ToString()}");

            Trace.TraceInformation($"Activity : {nameof(WorkflowRestorePoint<TOut>)} creating bookmark : {BookmarkName.Get(context)}, " +
                $"on WorkflowInstanceId: {context.WorkflowInstanceId.ToString()}");

            // Create a Bookmark and wait for it to be resumed.  
            context.CreateBookmark(BookmarkName.Get(context), new BookmarkCallback(OnResumeBookmark));
            Trace.Flush();
        }
    }
}
