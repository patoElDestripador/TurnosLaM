using System.ComponentModel.DataAnnotations;
namespace TurnosLaM.Models
{
    public class ShiftModel
    {
        [Key]
        public int Id { get;}
        public int PatientId { get; set; }  
        public int ServiceId { get; set; }  
        public DateTime CreationDate { get; set; }  
        public int Shift { get; set; }  
    }
}
