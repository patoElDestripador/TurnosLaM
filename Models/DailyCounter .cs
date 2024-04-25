namespace TurnosLaM.Models
{
    public class DailyCounter
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public string ServiceName { get; set; }
        public int Counter { get; set; }
    }
}
