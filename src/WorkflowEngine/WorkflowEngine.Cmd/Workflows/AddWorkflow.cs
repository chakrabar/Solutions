using System.Activities;

namespace WorkflowEngine.Cmd.Workflows
{
    public class AddWorkflow : CodeActivity
    {
        //[RequiredArgument]
        public InArgument<int> LeftVal { get; set; }

        //[RequiredArgument]
        public InArgument<int> RightVal { get; set; }

        public OutArgument<int> Sum { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Sum.Set(context, LeftVal.Get(context) + RightVal.Get(context));
        }
    }
}
