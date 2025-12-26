namespace DentalClinic.Api.DTOs.Dental
{
    public class DentalServiceReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Fee { get; set; }
    }
}
