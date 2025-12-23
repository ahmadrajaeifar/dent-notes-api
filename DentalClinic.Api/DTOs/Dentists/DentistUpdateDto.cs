using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Dentists
{
    public class DentistUpdateDto
    {
        public string Fullname { get; set; } = null!;
        public string? Password { get; set; }
    }
}
