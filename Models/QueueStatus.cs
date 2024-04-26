namespace TurnosLaM.Models
{
    public class QueueStatus
    {
        public int QueueId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName{ get; set; }
        public int PendingShifts{ get; set; }
        public string? UserStatus { get; set; }
        public string? Skills { get; set; }
    }
}
