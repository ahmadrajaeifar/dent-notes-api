using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.Entities;

namespace DentalClinic.Api.Repositories
{
    public class DentistRepository
    {
        private readonly AppDbContext _context;
        public DentistRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool UsernameExists(string username) =>
            _context.Dentists.Any(x => x.Username == username);

        public bool EmailExists(string email) =>
            _context.Dentists.Any(x => x.Email == email);

        public Dentist? AddDentist(Dentist dentist)
        {
            dentist.Id = _context.Dentists.Any() ?
                _context.Dentists.Max(x => x.Id) + 1 : 1;

            _context.Dentists.Add(dentist);
            _context.SaveChanges();
            return dentist;
        }

        public Dentist? GetDentistByUsername(string username) =>
            _context.Dentists.FirstOrDefault(x => x.Username == username);
    }
}
