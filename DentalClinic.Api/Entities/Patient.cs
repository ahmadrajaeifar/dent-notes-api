using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        [RegularExpression(@"\d{10}", ErrorMessage = "کد ملی باید 10 رقمی باشد")]
        public string NationalCode { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? Notes { get; set; }

        public int DentistId { get; set; } //FK
        public Dentist Dentist { get; set; } = null!;
    }
}
