using System;
using System.IO;

namespace WorkflowContainer.API.Helpers
{
    public class LogWriter
    {
        static readonly string _logFilepath = System.Web.Hosting.HostingEnvironment.MapPath("~/Writeline.log");

        public static void Log(string message)
        {
            File.AppendAllText(_logFilepath, $"{Environment.NewLine}" +
                $"{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fffff tt")} " +
                $"{Environment.NewLine}{message}");
        }
    }
}