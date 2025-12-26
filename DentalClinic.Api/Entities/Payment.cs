namespace DentalClinic.Api.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime PaidOn { get; set; }

        public PaymentType Type { get; set; }
    }

    public enum PaymentType
    {
        Cash = 1,
        Installment = 2
    }
}
