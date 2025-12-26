namespace DentalClinic.Api.Entities
{
    public class DentalServicePriceHistory
    {
        public int Id { get; set; }
        public int DentalServiceId { get; set; }
        public decimal Fee { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DentalService DentalService { get; set; } = null!;
    }
}
