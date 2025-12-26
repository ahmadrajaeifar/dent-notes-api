using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Entities;
using DentalClinic.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Api.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public Payment AddPayment(
            int invoiceId,
            decimal amount,
            PaymentMethod method,
            string? description = null)
        {
            var invoice = _context.Invoices
                .Include(x => x.Payments)
                .FirstOrDefault(x => x.Id == invoiceId);

            if (invoice == null)
                throw new Exception("صورتحساب بیمار یافت نشد!");

            if (amount <= 0)
                throw new Exception("مبلغ نمی تواند منفی یا صفر باشد!");

            if (amount > invoice.RemainingAmount)
                throw new Exception("مبلغ پرداخت شده بیش از بدهی باقیمانده است");

            var payment = new Payment
            {
                InvoiceId = invoiceId,
                Amount = amount,
                Method = method,
                Description = description
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return payment;
        }

        public PatientDebtDto GetPatientDebt(int patientId)
        {
            var patient = _context.Patients
                .Include(p => p.Invoices)
                    .ThenInclude(i => i.Items)
                .Include(p => p.Invoices)
                    .ThenInclude(i => i.Payments)
                .FirstOrDefault(p => p.Id == patientId);

            if (patient == null) throw new Exception("بیمار یافت نشد!");

            var total = patient.Invoices.Sum(i => i.TotalAmount);
            var paid = patient.Invoices.Sum(i => i.PaidAmount);

            return new PatientDebtDto
            {
                PatientId = patient.Id,
                Fullname = patient.FirstName + " " + patient.LastName,
                TotalDebt = total,
                PaidAmount = paid,
                RemainingAmount = total - paid
            };
        }
    }
}
