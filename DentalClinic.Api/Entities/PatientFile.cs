namespace DentalClinic.Api.Entities
{
    public class PatientFile
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public string FilePath { get; set; } = null!;
        public string? Description { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
