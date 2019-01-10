using System;
using System.Activities;
using System.Diagnostics;

namespace WorkflowContainer.Activities.Custom
{

    public sealed class CreateTask : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> RequestMessage { get; set; }

        [RequiredArgument]
        public InArgument<string> AsigneeEmail { get; set; }

        public OutArgument<Guid> TaskId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Trace.TraceInformation($"Executing {nameof(CreateTask)}");
            var taskId = Guid.NewGuid();
            TaskId.Set(context, taskId);

            Trace.TraceInformation($"CREATED TASK. Asignee: {AsigneeEmail.Get(context)}, " +
                $"RequestMessage: {RequestMessage.Get(context)}. " +
                $"Task ID: {taskId.ToString()}");
            Trace.Flush();
        }
    }
}
