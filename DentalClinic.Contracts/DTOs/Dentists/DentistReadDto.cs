using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Contracts.DTOs.Dentists
{
    public class DentistReadDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
