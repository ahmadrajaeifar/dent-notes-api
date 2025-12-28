namespace DentalClinic.Contracts.DTOs.Reports
{
    public class MonthlyIncomesDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
