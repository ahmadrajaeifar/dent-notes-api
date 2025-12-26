namespace DentalClinic.Api.DTOs.Dental
{
    public class DentalServiceCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Fee { get; set; }
    }
}
