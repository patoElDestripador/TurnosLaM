using System.ComponentModel.DataAnnotations;
namespace TurnosLaM.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
