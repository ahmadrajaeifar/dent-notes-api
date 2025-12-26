using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Api.Services
{
    public class PatientProcedureService
    {
        private readonly AppDbContext _context;

        public PatientProcedureService(AppDbContext context)
        {
            _context = context;
        }

        public PatientProcedure? AddService(
            PatientServiceCreateDto dto,
            int dentistId)
        {
            var patient = _context.Patients
                .FirstOrDefault(x => x.Id == dto.PatientId &&
                                     x.DentistId == dentistId);

            if (patient == null)
                return null;

            var tariff = _context.DentalServiceTariffs
                .FirstOrDefault(x =>
                    x.DentalServiceId == dto.DentalServiceId &&
                    x.Year == dto.ServiceDate.Year);

            if (tariff == null)
                return null;

            var patientService = new PatientProcedure
            {
                PatientId = dto.PatientId,
                DentalServiceId = dto.DentalServiceId,
                ServiceDate = dto.ServiceDate,
                FinalPrice = tariff.Price
            };

            _context.PatientServices.Add(patientService);
            _context.SaveChanges();

            return patientService;
        }

        public PatientDebtDto GetPatientDebt(int patientId)
        {
            var patient = _context.Patients
                .Include(p => p.Invoices)
                    .ThenInclude(i => i.Items)
                .Include(p => p.Invoices)
                    .ThenInclude(i => i.Payments)
                .FirstOrDefault(p => p.Id == patientId);

            if (patient == null) 
                throw new Exception("بیمار یافت نشد!");

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
