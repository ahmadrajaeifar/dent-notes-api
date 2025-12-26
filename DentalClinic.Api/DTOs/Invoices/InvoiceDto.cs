namespace DentalClinic.Api.DTOs.Invoices
{
    public class InvoiceDto
    {
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
