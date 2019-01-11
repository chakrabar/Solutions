using System;
using System.Activities;
using System.Diagnostics;

namespace WorkflowContainer.Activities.Custom
{
    public sealed class WorkflowRestorePoint<TOut> : NativeActivity<TOut>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object data)
        {
            Trace.TraceInformation($"Resuming : {nameof(WorkflowRestorePoint<TOut>)}, from bookmark : {bookmark.Name}");
            TOut output = default(TOut);
            if (data is TOut)
            {
                output = (TOut)data;
            }
            else
            {
                try
                {
                    //output = (TOut)Convert.ChangeType(data, typeof(TOut)); //fails when input is pure object (not this type)
                    output = JsonUtils.Cast<TOut>(data);

                    Trace.TraceInformation($"From : {nameof(WorkflowRestorePoint<TOut>)}. Bookmark resume data : {output.ToJson()}");

                    // When the Bookmark is resumed, assign its value to the Result argument
                    Result.Set(context, output);
                }
                catch (InvalidCastException e)
                {
                    Trace.TraceError($"From : {nameof(WorkflowRestorePoint<TOut>)}. Error casting data. Exception: {e.Message}");
                    Trace.Flush();
                    throw;
                }
            }
            Trace.Flush();
        }

        protected override void Execute(NativeActivityContext context)
        {
            Trace.TraceInformation($"Activity : {nameof(WorkflowRestorePoint<TOut>)} creating bookmark : {BookmarkName.Get(context)}, " +
                $"on WorkflowInstanceId: {context.WorkflowInstanceId.ToString()}");

            // Create a Bookmark and wait for it to be resumed.  
            context.CreateBookmark(BookmarkName.Get(context), new BookmarkCallback(OnResumeBookmark));
            Trace.Flush();
        }
    }
}
