using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TurnosLaM.Models
{
    public class Patient
    {
        public int Id { get; set;}
        public string? Document { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Eps { get; set; }
        
    }
}
