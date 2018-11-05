using AccessManagement.Domain.DTOs;
using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Enums;
using AccessManagement.External.Contracts;
using AccessManagement.External.DTOs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccessManagement.Application.ReportGenerators
{
    public class ReportGeneratorFacade : IReportGeneratorFacade
    {
        [Dependency]
        public IAccessPointService AccessPointService { get; set; }

        public void GenerateDailyReports()
        {
            var today = DateTime.Now;
            GetDailyAttendence(today);
        }

        public void GetDailyAttendence(DateTime date)
        {
            var logStartTime = GetDayStart(date);
            var logsForTheDay = AccessPointService.GetAccessDetailsSince(logStartTime);
            //save logs to database (need access point, empooyee) => Department, Site
            var employees = new List<Employee>(); //get from database or ldap?
            var accessPoints = new List<AccessPoint>(); //ger from DB

            var activityLogs = GetActivityLogs(logsForTheDay, employees, accessPoints);

            //dept wise activity
            var deptWiseActivity = GetDeptWiseActivity(activityLogs);

            //dept wise attendance
            var deptWiseAttendance = GetdeptWiseAttendance(activityLogs);
        }

        List<DeptWiseAttendanceDTO> GetdeptWiseAttendance(IEnumerable<ActivityLogDTO> activityLogs)
        {
            var deptWiseAttendance = activityLogs.GroupBy(al => al.Department.Id)
                                .Select(grp => new DeptWiseAttendanceDTO
                                {
                                    Department = grp.First().Department.Name,
                                    Attendance = grp.GroupBy(gd => gd.Employee.Id)
                                                    .Select(empGroup => new AttendanceDTO
                                                    {
                                                        EmployeeName = empGroup.First().Employee.Name,
                                                        EmployeeId = empGroup.First().Employee.Id,
                                                        Attended = true,
                                                        Date = empGroup.First().TimeStamp.ToString("yyyy-MM-dd")
                                                    })
                                                    .ToList()
                                })
                                .ToList();
            return deptWiseAttendance;
        }

        List<DeptWiseActivityDTO> GetDeptWiseActivity(IEnumerable<ActivityLogDTO> activityLogs)
        {
            var deptWiseActivity = activityLogs.GroupBy(al => al.Department.Id)
                                .Select(grp => new DeptWiseActivityDTO
                                {
                                    Department = grp.First().Department.Name,
                                    ManagerName = grp.First().Department.Manager.Name,
                                    ManagerEmail = grp.First().Department.Manager.Email,
                                    ActivityLogs = grp.Select(g => new ActivityReportDTO
                                    {
                                        AccessPoint = g.AccessPoint.Name,
                                        AccesType = g.Type.ToString(),
                                        Employee = g.Employee.Name,
                                        Facility = g.AccessPoint.Facility.Name,
                                        TimeStamp = g.TimeStamp.ToString("yyyy-MM-dd hh:mm:ss")
                                    })
                                    .ToList()
                                })
                                .ToList();
            return deptWiseActivity;
        }

        IEnumerable<ActivityLogDTO> GetActivityLogs(IEnumerable<LogEntryDTO> logs, IEnumerable<Employee> employees, IEnumerable<AccessPoint> accessPoints)
        {
            var activityLogs = new List<ActivityLogDTO>();
            foreach (var log in logs)
            {
                var employee = employees.FirstOrDefault(e => e.Id == log.EmployeeId);
                if (employees != null)
                {
                    activityLogs.Add(new ActivityLogDTO
                    {
                        Employee = employee,
                        Department = employee.Deprtment,
                        Id = log.LogId,
                        TimeStamp = log.TimeStamp,
                        Type = GetAccessType(log.Type),
                        AccessPoint = accessPoints.FirstOrDefault(a => a.Id == log.AccessPointId)
                    });
                }
            }
            return activityLogs;
        }

        AccessType GetAccessType(string access)
        {
            if (string.IsNullOrEmpty(access))
                return AccessType.None;
            if (access.ToLower().Contains("entry") || access.ToLower().Contains("enter"))
                return AccessType.AccessEntry;
            if (access.ToLower().Contains("exit"))
                return AccessType.AccessExit;
            if (access.ToLower().Contains("deny") || access.ToLower().Contains("denied") || access.ToLower().Contains("stop"))
                return AccessType.AccessDenied;
            return AccessType.None;
        }

        DateTime GetDayStart(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
