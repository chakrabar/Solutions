using Microsoft.VisualBasic.Activities;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Statements;

namespace WorkflowContainer.Activities
{
    class SampleActivityBuilder
    {
        public static ActivityBuilder<int> GetActivityBuilder()
        {
            ActivityBuilder<int> ab = new ActivityBuilder<int>();
            ab.Name = "Add";
            ab.Properties.Add(new DynamicActivityProperty { Name = "Operand1", Type = typeof(InArgument<int>) });
            ab.Properties.Add(new DynamicActivityProperty { Name = "Operand2", Type = typeof(InArgument<int>) });
            ab.Implementation = new Sequence
            {
                Activities =
                {
                    new WriteLine
                    {
                        Text = new VisualBasicValue<string>("Operand1.ToString() + \" + \" + Operand2.ToString()")
                    },
                    new Assign<int>
                    {
                        To = new ArgumentReference<int> { ArgumentName = "Result" },
                        Value = new VisualBasicValue<int>("Operand1 + Operand2")
                    }
                }
            };
            return ab;
        }
    }
}
