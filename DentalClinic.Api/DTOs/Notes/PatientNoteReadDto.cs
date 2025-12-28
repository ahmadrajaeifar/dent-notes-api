namespace DentalClinic.Api.DTOs.Notes
{
    public class PatientNoteReadDto
    {
        public int Id { get; set; }
        public string Note { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
