using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.Entities;
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

        public Patient? GetPatientById(int id) =>
            _context.Patients.FirstOrDefault(p => p.Id == id);

        public void AddPatient(Patient patient)
        {
            patient.Id = _context.Patients.Any() ? 
                _context.Patients.Max(x => x.Id) + 1 : 1;

            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void EditPatient(Patient patient)
        {
            var existing = _context.Patients.FirstOrDefault(x => x.Id == patient.Id);
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

        public void DeletePatient(int id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.Id == id);
            if (patient == null) 
                return;
            
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }
    }
}
