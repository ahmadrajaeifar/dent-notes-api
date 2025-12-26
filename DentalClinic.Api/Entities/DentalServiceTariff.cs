namespace DentalClinic.Api.Entities
{
    public class DentalServiceTariff
    {
        public int Id { get; set; }

        public int Year { get; set; }          // 1404
        public decimal Price { get; set; }

        public int DentalServiceId { get; set; }
        public DentalService DentalService { get; set; } = null!;
    }
}
