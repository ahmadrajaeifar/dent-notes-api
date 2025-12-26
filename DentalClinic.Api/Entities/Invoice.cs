namespace DentalClinic.Api.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public int PatientId { get; set; } //FK
        public Patient Patient { get; set; } = null!;

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount => Items.Sum(x => x.TotalPrice);
        public decimal PaidAmount => Payments.Sum(x => x.Amount);
        public decimal RemainingAmount => TotalAmount - PaidAmount;
    }
}
