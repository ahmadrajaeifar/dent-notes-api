using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.Entities;

namespace DentalClinic.Api.Services
{
    public class PatientNoteService
    {
        private readonly AppDbContext _context;

        public PatientNoteService(AppDbContext context)
        {
            _context = context;
        }

        public PatientNote AddNote(
            int patientId,
            int dentistId,
            string note)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == patientId && p.DentistId == dentistId);

            if (patient == null)
                throw new Exception("Patient not found");

            var patientNote = new PatientNote
            {
                PatientId = patientId,
                DentistId = dentistId,
                Note = note
            };

            _context.PatientNotes.Add(patientNote);
            _context.SaveChanges();

            return patientNote;
        }

        public List<PatientNote> GetNotes(int patientId, int dentistId)
        {
            return _context.PatientNotes
                .Where(n => n.PatientId == patientId && n.DentistId == dentistId)
                .OrderByDescending(n => n.CreatedOn)
                .ToList();
        }
    }
}
