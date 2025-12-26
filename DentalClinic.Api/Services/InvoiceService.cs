using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.Entities;

namespace DentalClinic.Api.Services
{
    public class InvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public Invoice? GenerateInvoice(int patientId, int dentistId)
        {
            var patient = _context.Patients
                .FirstOrDefault(x => x.Id == patientId && 
                    x.DentistId == dentistId && !x.IsDeleted);

            if (patient == null)
                return null;

            var totalAmount = _context.PatientServices
                .Where(x => x.PatientId == patientId)
                .Sum(x => x.FinalPrice);

            var paidAmount = _context.Payments
                .Where(x => x.PatientId == patientId)
                .Sum(x => x.Amount);

            return new Invoice
            {
                PatientId = patientId,
                TotalAmount = totalAmount,
                PaidAmount = paidAmount
            };
        }
    }

}
