using DentalClinic.Api.Enums;

namespace DentalClinic.Api.DTOs.Payments
{
    public class PaymentReadDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;
    }
}
