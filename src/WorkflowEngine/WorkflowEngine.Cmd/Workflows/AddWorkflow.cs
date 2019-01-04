using System.Activities;

namespace WorkflowEngine.Cmd.Workflows
{
    public class AddWorkflow : CodeActivity<int>
    {
        [RequiredArgument]
        public InArgument<int> LeftVal { get; set; }

        [RequiredArgument]
        public InArgument<int> RightVal { get; set; }

        protected override int Execute(CodeActivityContext context)
        {
            return LeftVal.Get(context) + RightVal.Get(context);
        }
    }
}
