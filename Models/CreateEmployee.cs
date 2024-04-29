using System.ComponentModel.DataAnnotations;
namespace TurnosLaM.Models
{
    public class CreateEmployee
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [MaxLength(50)]
        public string? Status { get; set; }
        public string? Password { get; set; }
        public DateTime CreationTime { get; set; }
        public string? Role { get; set; }
        //Se agregan las habilidades de los usuarios
        public bool Skill1 { get; set; }
        public bool Skill2 { get; set; }
        public bool Skill3 { get; set; }
        public bool Skill4 { get; set; }
    }
}
