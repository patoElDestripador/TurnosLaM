namespace TurnosLaM.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? CreationDate { get; set; }
        //Agregamos las habiliades al modelo que itera en el foreach de empleados
         public bool Skill1 { get; set; }
        public bool Skill2 { get; set; }
        public bool Skill3 { get; set; }
        public bool Skill4 { get; set; }
    }
}