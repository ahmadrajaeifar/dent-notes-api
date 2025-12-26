namespace DentalClinic.Api.DTOs.Dentists
{
    public class MonthlyIncomeDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
