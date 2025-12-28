namespace DentalClinic.Api.Entities
{
    public class PatientNote
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public Patient Patient { get; set; } = null!;

        public string Note { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
