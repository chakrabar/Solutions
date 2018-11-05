
namespace AccessManagement.Domain.DTOs
{
    public class AttendanceDTO
    {
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public bool Attended { get; set; }
        public string Date { get; set; }
    }
}
