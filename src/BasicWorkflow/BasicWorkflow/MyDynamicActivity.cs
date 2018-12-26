using Microsoft.VisualBasic.Activities;
using System.Activities;
using System.Activities.Statements;

namespace BasicWorkflow
{
    public class MyDynamicActivity
    {
        public void CreateActivity()
        {
            DynamicActivity d = new DynamicActivity
            {
                Properties =
                {
                    new DynamicActivityProperty
                    {
                        Name = "greeting",
                        Type = typeof(InArgument<string>)
                    }
                },
                Implementation = () => new WriteLine
                {
                    Text = new VisualBasicValue<string>("greeting")
                }
            };
        }
    }
}
