namespace DentalClinic.Api.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Entity { get; set; } = null!;
        public int EntityId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string PerformedBy { get; set; } = null!;
    }
}
