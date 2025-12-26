using DentalClinic.Api.Enums;

namespace DentalClinic.Api.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }
    }
}
