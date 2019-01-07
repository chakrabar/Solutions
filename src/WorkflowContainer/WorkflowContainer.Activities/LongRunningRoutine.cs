using System;
using System.Activities;
using System.Activities.Statements;

namespace WorkflowContainer.Activities
{
    public class LongRunningRoutine
    {
        public static Activity Get()
        {
            Variable<string> approval1 = new Variable<string>
            {
                Name = "approval1"
            };
            Variable<string> approval2 = new Variable<string>
            {
                Name = "approval2"
            };
            Variable<int> amount = new Variable<int>
            {
                Name = "amount", Default = new Random().Next(1, 100)
            };
            Variable<int> amount2 = new Variable<int> //TODO: for test of data (variable) flow across bookmarks (=> worked)
            {
                Name = "amount2"
            };

            var secondLevelApproval = new Sequence
            {
                Variables =
                {
                    approval2, amount2
                },
                Activities =
                {
                    new Assign<int>
                    {
                        To = amount2,
                        Value = new InArgument<int>((e) => amount.Get(e)) //this works
                    },
                    new WriteLine
                    {
                        Text = "Approved by first level approver, requesting approver 2."
                    },
                    new RequestHumanInput<string>
                    {
                        DisplayName = "Second Level Approval",
                        BookmarkName = "SecondLevelApproval",
                        RequestMessage = new InArgument<string>((env) => "Hi Approver 2, Can you please approve amount " + amount.Get(env).ToString()),
                        Result = approval2
                    },
                    new If()
                    {
                        Condition = new InArgument<bool>((e) => approval2.Get(e).ToLower() == "approve" || approval2.Get(e).ToLower() == "approved"),
                        Then = new WriteLine
                        {
                            Text = "The invoice was APPROVED. Process completed."
                        },
                        Else = new WriteLine
                        {
                            Text = "The invoice was REJECTED by Approver 2"
                        }
                    }
                }
            };

            Activity wf = new Sequence
            {
                Variables =
                {
                    approval1, amount
                },
                Activities =
                {
                    new WriteLine()
                    {
                        Text = new InArgument<string>((env) => "The invoice amount is : " + amount.Get(env).ToString())
                    },
                    new RequestHumanInput<string>
                    {
                        DisplayName = "First Level Approval",
                        BookmarkName = "FirstLevelApproval",
                        RequestMessage = new InArgument<string>((env) => "Hi Approver 1, Can you please approve amount " + amount.Get(env).ToString()),
                        Result = approval1
                    },
                    new If()
                    {
                        Condition = new InArgument<bool>((e) => approval1.Get(e).ToLower() == "approved"),
                        Then = secondLevelApproval,
                        Else = new WriteLine
                        {
                            Text = new InArgument<string>((env) => "The invoice was REJECTED by Approver 1. Amount : " + amount.Get(env).ToString())
                        }
                    }
                }
            };

            return wf;
        }
    }
}
