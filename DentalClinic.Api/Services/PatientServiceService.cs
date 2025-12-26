using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Patients;

namespace DentalClinic.Api.Services
{
    public class PatientServiceService
    {
        private readonly AppDbContext _context;

        public PatientServiceService(AppDbContext context)
        {
            _context = context;
        }

        public PatientService? AddService(
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

            var patientService = new PatientService
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
    }
}
