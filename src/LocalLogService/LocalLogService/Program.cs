﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LocalLogService
{
    public class Program
    {
        public static void Main2(string[] args)
        {
            CreateWebHostBuilder2(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder2(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = CreateWebHostBuilder(
                args
                .Where(arg => arg != "--console")
                .ToArray());

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                builder.UseContentRoot(pathToContentRoot);
            }

            var host = builder.Build();

            if (isService)
            {
                // To run the app without the CustomWebHostService change the
                // next line to host.RunAsService();
                // host.RunAsCustomService(); //commented - no custom web service
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddEventLog();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                // Configure the app here.
            })
                .UseStartup<Startup>();
    }
}
