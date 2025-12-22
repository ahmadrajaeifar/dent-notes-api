namespace DentalClinic.Api.DTOs.Patients
{
    public class PatientReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; };
        public DateTime CreatedOn { get; set; }
        public string? Notes { get; set; }
    }
}
