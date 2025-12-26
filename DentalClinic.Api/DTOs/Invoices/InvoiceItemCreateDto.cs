namespace DentalClinic.Api.DTOs.Invoices
{
    public class InvoiceItemCreateDto
    {
        public int DentalServiceId { get; set; }
        public decimal Price { get; set; }
    }
}
