
using TurnosLaM.Data;
using TurnosLaM.Models;


namespace TurnosLaM.Models
{
    public class  Pacientqueue
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EmployeesId { get; set; }
        public int ShiftId { get; set; }
        public string? Status { get; set; }
        public string? AssignedShift { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public DateTime? ClosingTime { get; set; }
        public int? Calls { get; set; }
        public string? Document { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ServiceName { get; set; }
        public string? Eps { get; set; }
        public bool IsRegistered { get; set; } 
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        
        
    }
}