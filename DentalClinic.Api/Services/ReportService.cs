using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Incomes;

namespace DentalClinic.Api.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public IncomeReportDto GetDentistIncome(int dentistId)
        {
            var invoices = _context.Invoices
                .Where(i => i.Patient.DentistId == dentistId);

            var totalAmount = invoices.Sum(i => i.TotalAmount);
            var totalPaid = invoices.Sum(i => i.PaidAmount);

            return new IncomeReportDto
            {
                TotalIncome = totalAmount,
                TotalPaid = totalPaid,
                TotalDebt = totalAmount - totalPaid
            };
        }
    }
}
