using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWorkflow
{
    public class SendMailWithFunc : NativeActivity
    {
        public InArgument<string> RecipientAddress { get; set; }
        public InArgument<string> FromAddress { get; set; }
        public InArgument<string> Subject { get; set; }

        //this is the remplate - use a method to populate the mail body
        [Browsable(false)] //this property should not show up in designer property grid
        public ActivityFunc<string> CreateBodyText { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            if (CreateBodyText != null)
            {
                context.ScheduleFunc<string>(
                    CreateBodyText, BodyCreated);
            }
            else
            {
                SendMailInternal(context, null);
            }
        }

        private void SendMailInternal(NativeActivityContext context, string mailBody)
        {
            var mail = $"{Environment.NewLine}From: {FromAddress.Get(context)}" +
                $"{Environment.NewLine}To: {RecipientAddress.Get(context)}" +
                $"{Environment.NewLine}Subject: {Subject.Get(context)}" +
                $"{Environment.NewLine}Body: {mailBody}";
            Console.WriteLine(mail);
        }

        private void BodyCreated(NativeActivityContext context, ActivityInstance completedInstance, string data)
        {
            SendMailInternal(context, data);
        }
    }
}
