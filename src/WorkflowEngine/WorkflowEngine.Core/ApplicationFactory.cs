using System;
using System.Activities;

namespace WorkflowEngine.Core
{
    public class ApplicationFactory
    {
        public void Get(Activity activity)
        {
            WorkflowApplication wfApp = new WorkflowApplication(activity);

            Console.WriteLine("Id: {0}", wfApp.Id);
        }
    }
}
