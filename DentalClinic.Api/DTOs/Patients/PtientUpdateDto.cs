using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Patients
{
    public class PtientUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [RegularExpression(@"\d{10}", 
            ErrorMessage = "کد ملی باید 10 رقمی باشد")]
        public string? NationalCode { get; set; }
        public string? Notes { get; set; }
    }
}
