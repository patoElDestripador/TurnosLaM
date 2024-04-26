namespace TurnosLaM.Models
{
    public class ShiftModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }  
        public int ServiceId { get; set; }  
        public DateTime CreationDate { get; set; }  
        public int Shift { get; set; }  
    }
}
