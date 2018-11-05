using System;

namespace AccessManagement.Application.ReportGenerators
{
    public interface IReportGeneratorFacade
    {
        void GenerateDailyReports();
        void GetDailyAttendence(DateTime date);
    }
}
