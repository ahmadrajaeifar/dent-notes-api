using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Dentists
{
    public class DentistCreateDto
    {
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        [Required, EmailAddress(ErrorMessage =
            "به منظور بازیابی رمز فراموش شده، وارد کردن ایمیل معتبر، ضروری است")]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
