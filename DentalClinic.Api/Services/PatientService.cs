using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Entities;
using DentalClinic.Api.Middlewares;
using DentalClinic.Api.Repositories;

namespace DentalClinic.Api.Services
{
    public class PatientService
    {
        private readonly DentistService _service;
        private readonly PatientRepository _repo;
        public PatientService(
            DentistService service,
            PatientRepository repo)
        {
            _service = service;
            _repo = repo;
        }

        public Patient? GetPatientById(int patientId, int dentistId)
        {
            var patient = _repo.GetPatientById(patientId);
            
            if (patient == null) return null;

            if (patient.DentistId != dentistId)
                return null;

            return patient;
        }

        public class PaginatedResult<T>
        {
            public List<T> Items { get; set; } = new();
            public int TotalItems { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPages =>
                (int)Math.Ceiling(TotalItems / (double)PageSize);
        }

        public List<Patient> GetAllPatients() =>
            _repo.GetAllPatients();

        public IEnumerable<Patient> GetPatientsByDentist(int dentistId, 
            PaginationParams pagination)
        {
            return _repo.GetPatientsByDentist(dentistId, pagination)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
        }

        public (IEnumerable<Patient> patients, int TotalCount)
            GetPatientsByDentistWithCount(int dentistId, PaginationParams pagination)
        {
            var query = _repo.GetPatientsByDentist(dentistId, pagination);
            
            var totalCount = query.Count();

            var patients = query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return (patients, totalCount);
        }

        public Patient CreatePatient(PatientCreateDto dto, int dentistId)
        {
            if (_repo.NationalCodeExists(dto.NationalCode, dentistId))
                throw new BusinessException("بیماری با این کد ملی قبلاً ثبت شده است");

            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                NationalCode = dto.NationalCode,
                Email = dto.Email,
                DentistId = dentistId,
                CreatedOn = DateTime.UtcNow
            };

            _repo.AddPatinet(patient);
            return patient;
        }

        public Patient? UpdatePatient(
            int id, 
            PatientUpdateDto dto, 
            int dentistId)
        {
            var patient = _repo.GetPatientById(id);


            if (patient == null || patient.DentistId != dentistId) 
                return null;

            patient.FirstName = dto.FirstName.Trim();
            patient.LastName = dto.LastName.Trim();
            patient.PhoneNumber = dto.PhoneNumber ?? patient.PhoneNumber;
            patient.NationalCode = dto.NationalCode ?? patient.NationalCode;
            patient.Email = dto.Email ?? patient.Email;

            _repo.EditPatient(patient);
            return patient;
        }

        public bool DeletePatient(int id, int dentistId)
        {
            var patient = _repo.GetPatientById(id);
            if (patient == null)
                return false;

            if (patient.DentistId != dentistId)
                return false;

            _repo.DeletePatient(id, dentistId);
            return true;
        }
    }
}
