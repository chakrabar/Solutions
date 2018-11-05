using AccessManagement.External.Contracts;
using AccessManagement.Application.CommandStack;
using AccessManagement.Application.ReportGenerators;
using Microsoft.Practices.Unity;

namespace AccessManagement.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Initializer.Bootstrap();
            
            //load DB with LDAP data
            var ldapSync = Initializer.Container.Resolve<ILdapSyncFacade>();
            ldapSync.SyncFromLdap();

            //load DB access point details
            var accessSync = Initializer.Container.Resolve<IAccessPointFacade>();
            accessSync.SyncAccessPoints();

            //generate reports
            var reportGenerator = Initializer.Container.Resolve<IReportGeneratorFacade>();
            reportGenerator.GenerateDailyReports();

            System.Console.ReadLine();
        }
    }
}
