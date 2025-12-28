using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Contracts.DTOs.Dentists
{
    public class DentistCreateDto
    {
        [Required, MinLength(5)]
        public string Fullname { get; set; } = null!;
        [Required, MinLength(3)]
        public string Username { get; set; } = null!;
        [Required, EmailAddress(ErrorMessage =
            "به منظور بازیابی رمز فراموش شده، وارد کردن ایمیل معتبر، ضروری است")]
        public string Email { get; set; } = null!;
        [Required, MinLength(6)]
        public string Password { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
