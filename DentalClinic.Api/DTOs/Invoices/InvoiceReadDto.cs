namespace DentalClinic.Api.DTOs.Invoices
{
    public class InvoiceReadDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }

}
