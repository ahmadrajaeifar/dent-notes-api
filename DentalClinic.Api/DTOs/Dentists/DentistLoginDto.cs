using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Dentists
{
    public class DentistLoginDto
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
