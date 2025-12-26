using DentalClinic.Api.Entities;
using DentalClinic.Api.Enums;

namespace DentalClinic.Api.DTOs.Payments
{
    public class PaymentCreateDto
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string? Description { get; set; }
    }
}
