using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Patients
{
    public class PatientUpdateDto
    {
        [Required(ErrorMessage = "نام الزامی است")]
        [StringLength(50, MinimumLength = 2,
        ErrorMessage = "نام باید بین ۲ تا ۵۰ کاراکتر باشد")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [StringLength(10, MinimumLength = 10, 
            ErrorMessage = "کد ملی باید ۱۰ رقم باشد")]
        public string? NationalCode { get; set; }

        [Phone(ErrorMessage = "شماره تماس نامعتبر است")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
        public string? Email { get; set; }
    }
}
