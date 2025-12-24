using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Dentists
{
    public class DentistUpdateDto
    {
        [Required, MinLength(3)]
        public string Fullname { get; set; } = null!;
        [MinLength(6)]
        public string? Password { get; set; }
    }
}
