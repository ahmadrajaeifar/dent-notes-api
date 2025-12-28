using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Entities;
using DentalClinic.Api.Middlewares;
using DentalClinic.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using static DentalClinic.Api.DTOs.Common.PaginationParams;

namespace DentalClinic.Api.Services
{
    public class PatientService
    {
        private readonly DentistService _service;
        private readonly PatientRepository _repo;
        private readonly AppDbContext _context;

        public PatientService(
            DentistService service,
            PatientRepository repo,
            AppDbContext context)
        {
            _service = service;
            _repo = repo;
            _context = context;
        }

        public Patient? GetPatientById(int patientId, int dentistId)
        {
            var patient = _repo.GetPatientById(patientId);

            if (patient == null) return null;

            if (patient.DentistId != dentistId)
                return null;

            return patient;
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

        public PagedResult<Patient> SearchPatients(
            PatientSearchDto dto,
             PaginationParams pagination,
            int dentistId)
        {
            var query = _context.Patients
                .Where(p => p.DentistId == dentistId
                && !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                query = query.Where(p => p.FirstName.Contains(dto.FirstName)
                && !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                query = query.Where(p => p.LastName.Contains(dto.LastName)
                && !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(dto.NationalCode))
                query = query.Where(p => p.NationalCode == dto.NationalCode
                && !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                query = query.Where(p => p.PhoneNumber.Contains(dto.PhoneNumber)
                && !p.IsDeleted);

            var totalCount = query.Count();

            var items = query
                .OrderByDescending(p => p.Id)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return new PagedResult<Patient>
            {
                Items = items,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount
            };
        }

        public PatientFile AddFile(
            int patientId,
            string filePath,
            string? description,
            int dentistId)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == patientId && p.DentistId == dentistId
                && !p.IsDeleted);

            if (patient == null)
                throw new Exception("بیمار یافت نشد!");

            var file = new PatientFile
            {
                PatientId = patientId,
                FilePath = filePath,
                Description = description
            };

            _context.PatientFiles.Add(file);
            _context.SaveChanges();

            return file;
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

        public PatientNote AddNote(
            int patientId,
            string note,
            int dentistId)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == patientId && p.DentistId == dentistId);

            if (patient == null)
                throw new Exception("بیمار یافت نشد!");

            var patientNote = new PatientNote
            {
                PatientId = patientId,
                Note = note
            };

            _context.PatientNotes.Add(patientNote);
            _context.SaveChanges();

            return patientNote;
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
