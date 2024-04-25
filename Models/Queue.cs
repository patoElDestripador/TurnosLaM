using System.ComponentModel.DataAnnotations;
using TurnosLaM.Data;

namespace TurnosLaM.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public string? Status { get; set; }
        public string? AssignedShift { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public DateTime? ClosingTime { get; set; }
        public int? Calls { get; set; }
        public  DateTime StartTime { get; set; }
    }
}
