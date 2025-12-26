namespace DentalClinic.Api.Entities
{
    public class DentalService
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Fee { get; set; }             // حق الزحمه فعلی
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public ICollection<DentalServicePriceHistory> PriceHistories { get; set; } = new List<DentalServicePriceHistory>();
    }
}
