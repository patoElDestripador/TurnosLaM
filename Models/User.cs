namespace TurnosLaM.Models
{
    public class User
    {
        public int Id { get; set;}
        public int EmployeesId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Module { get; set; }
        public string? Status { get; set; }
        public string? Skills { get; set; } //MTO= Este no lo cambiamos por que por defecto recibe datos de tipo String 
    }
}
