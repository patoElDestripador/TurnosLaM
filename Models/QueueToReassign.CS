namespace TurnosLaM.Models
{
    public class QueueToReassign
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }
        public string? AssociatedService { get; set; }
    }
}
