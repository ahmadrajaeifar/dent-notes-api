using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Patients
{
    public class PatientCreateDto
    {
        [Required(ErrorMessage = "نام الزامی است")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        public string LastName { get; set; } = null!;
        [Phone(ErrorMessage = "شماره تماس نامعتبر است")]
        public string PhoneNumber { get; set; } = null!;
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "کد ملی الزامی است")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید ۱۰ رقم باشد")]
        public string NationalCode { get; set; } = null!;
    }
}
