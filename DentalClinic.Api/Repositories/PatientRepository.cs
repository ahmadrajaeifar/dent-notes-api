using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Xml;

namespace DentalClinic.Api.Repositories
{
    public class PatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAllPatients() =>
            _context.Patients.ToList();

        public List<Patient> GetPatientsByDentistId(
            int dentistId, 
            int pageNumber, 
            int pageSize)
        {
            return _context.Patients
                .Where(p => p.DentistId == dentistId && !p.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IQueryable<Patient> GetPatientsByDentist(
            int dentistId,
            PaginationParams pagination)
        {
            var query = _context.Patients
                .Where(p => p.DentistId == dentistId && !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(pagination.Search))
            {
                var search = pagination.Search.Trim();

                query = query.Where(p =>
                    p.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.NationalCode.Contains(search) ||
                    p.PhoneNumber.Contains(search));
            }

            query = pagination.SortBy?.ToLower() switch
            {
                "firstname" => pagination.SortOrder == "asc"
                    ? query.OrderBy(p => p.FirstName)
                    : query.OrderByDescending(p => p.FirstName),

                "lastname" => pagination.SortOrder == "asc"
                    ? query.OrderBy(p => p.LastName)
                    : query.OrderByDescending(p => p.LastName),

                "created" => pagination.SortOrder == "asc"
                    ? query.OrderBy(p => p.CreatedOn)
                    : query.OrderByDescending(p => p.CreatedOn),

                _ => query.OrderByDescending(p => p.Id)
            };

            return query.OrderByDescending(p => p.Id);
        }

        public int GetTotalPatientsByDentistId(int dentistId) =>
            _context.Patients.Count(p => p.DentistId == dentistId && !p.IsDeleted);

        public Patient? GetPatientById(int id) =>
            _context.Patients.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

        public void AddPatinet(Patient patient)
        {
            patient.Id = _context.Patients.Any() ? 
                _context.Patients.Max(x => x.Id) + 1 : 1;

            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public bool NationalCodeExists(
            string nationalCode, 
            int dentistId)
        {
            return _context.Patients.Any(p => 
                p.NationalCode == nationalCode && 
                p.DentistId == dentistId);
        }

        public void EditPatient(Patient patient)
        {
            var existing = _context.Patients
                .FirstOrDefault(x => x.Id == patient.Id && ! x.IsDeleted);

            if (existing == null)
                return;

            existing.FirstName = patient.FirstName;
            existing.LastName = patient.LastName;
            existing.PhoneNumber = patient.PhoneNumber;
            existing.Email = patient.Email;
            existing.NationalCode = patient.NationalCode;
            existing.DentistId = patient.DentistId;

            _context.SaveChanges();
        }

        public bool DeletePatient(int id, int dentistId)
        {
            var patient = _context.Patients
                .FirstOrDefault(x => x.Id == id && x.DentistId == dentistId);

            if (patient == null) 
                return false;

            patient.IsDeleted = true;
            
            _context.SaveChanges();

            return true;
        }
    }
}
