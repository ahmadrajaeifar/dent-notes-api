namespace DentalClinic.Api.DTOs.Invoices
{
    public class InvoiceCreateDto
    {
        public int PatientId { get; set; }
        public List<InvoiceItemCreateDto> Items { get; set; } = new();
    }
}
