namespace DentalClinic.Api.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public decimal RemainingAmount => TotalAmount - PaidAmount;
    }
}
