namespace DentalClinic.Api.DTOs.Patients
{
    public class PatientDebtDto
    {
        public int PatientId { get; set; }
        public string Fullname { get; set; } = null!;
        public decimal TotalDebt { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
