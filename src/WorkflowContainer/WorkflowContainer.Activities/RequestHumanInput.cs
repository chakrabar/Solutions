﻿using System;
using System.Activities;
using System.Diagnostics;

namespace WorkflowContainer.Activities
{
    public sealed class RequestHumanInput<T> : NativeActivity<T>
    {
        [RequiredArgument]
        public InArgument<string> RequestMessage { get; set; }

        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }        

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            Trace.TraceInformation($"From : {nameof(RequestHumanInput<T>)}. Re-starting bookmark : {bookmark.Name}");
            T output = default(T);
            if (obj is T)
            {
                output = (T)obj;
            }
            try
            {
                output = (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (InvalidCastException e)
            {
                Trace.TraceError($"From : {nameof(RequestHumanInput<T>)}. Error casting data. Exception: {e.Message}");
                throw;
            }

            Trace.TraceInformation($"From : {nameof(RequestHumanInput<T>)}. Bookmark resume data : {output.ToString()}");
            // When the Bookmark is resumed, assign its value to the Result argument
            Result.Set(context, output);
            Trace.Flush();
        }

        protected override void Execute(NativeActivityContext context)
        {
            // Send request to human
            Trace.TraceInformation($"From : {nameof(RequestHumanInput<T>)}. CREATE TASK >> {RequestMessage.Get(context)}");

            Trace.TraceInformation($"From : {nameof(RequestHumanInput<T>)}. Creating bookmark : {BookmarkName.Get(context)}, " +
                $"on WorkflowInstanceId: {context.WorkflowInstanceId.ToString()}");

            // Create a Bookmark and wait for it to be resumed.  
            context.CreateBookmark(BookmarkName.Get(context),
                new BookmarkCallback(OnResumeBookmark));
            Trace.Flush();
        }
    }
}
