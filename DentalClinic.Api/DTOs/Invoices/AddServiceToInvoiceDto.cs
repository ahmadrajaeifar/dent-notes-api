namespace DentalClinic.Api.DTOs.Invoices
{
    public class AddServiceToInvoiceDto
    {
        public int DentalServiceId { get; set; }
        public decimal Price { get; set; }
    }
}
