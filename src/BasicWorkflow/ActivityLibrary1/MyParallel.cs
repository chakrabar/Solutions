using System;
using System.Activities;
using System.Collections.ObjectModel;

namespace ActivityLibrary1
{

    public sealed class MyParallel : NativeActivity
    {
        private Collection<Activity> _children = new Collection<Activity>();

        public Collection<Activity> Branches
        {
            get { return _children; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            foreach (var child in _children)
            {
                context.ScheduleActivity(child);
            }
        }
    }

    public sealed class MyParallel2 : NativeActivity
    {
        private Collection<Activity> _children = new Collection<Activity>();
        private CompletionCallback _completed;

        public Collection<Activity> Branches
        {
            get { return _children; }
        }

        public MyParallel2()
        {
            _completed = new CompletionCallback(Activity_Completed);
        }

        private void Activity_Completed(NativeActivityContext context, 
            ActivityInstance activity)
        {
            Console.WriteLine($"{activity.Activity.DisplayName} completed");
        }

        protected override void Execute(NativeActivityContext context)
        {
            foreach (var child in _children)
            {
                context.ScheduleActivity(child, Activity_Completed);
            }
        }
    }
}
