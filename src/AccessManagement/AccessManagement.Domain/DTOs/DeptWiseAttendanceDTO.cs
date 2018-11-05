using System.Collections.Generic;

namespace AccessManagement.Domain.DTOs
{
    public class DeptWiseAttendanceDTO
    {
        public string Department { get; set; }
        public List<AttendanceDTO> Attendance { get; set; }
    }
}
