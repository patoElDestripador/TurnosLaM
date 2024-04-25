using System.ComponentModel.DataAnnotations;
namespace TurnosLaM.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public string? Status { get; set; }
    }
}
