using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Entities
{
    public class Dentist
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        [Required, EmailAddress(ErrorMessage =
            "به منظور بازیابی رمز فراموش شده، وارد کردن ایمیل معتبر، ضروری است")]
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
