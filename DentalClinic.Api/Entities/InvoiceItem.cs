namespace DentalClinic.Api.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        public int DentalServiceId { get; set; }
        public DentalService DentalService { get; set; } = null!;

        // قیمت در زمان انجام خدمت (Snapshot)
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal TotalPrice => Price * Quantity;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
