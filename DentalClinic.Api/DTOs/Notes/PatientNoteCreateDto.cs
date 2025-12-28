using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.DTOs.Notes
{
    public class PatientNoteCreateDto
    {
        [Required, MinLength(3)]
        public string Note { get; set; } = null!;
    }
}
