using DentalClinic.Api.DTOs.Dentists;
using DentalClinic.Api.Entities;
using DentalClinic.Api.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace DentalClinic.Api.Services
{
    public class DentistService
    {
        private readonly DentistRepository _repo;
        public DentistService(DentistRepository repo)
        {
            _repo = repo;
        }

        public Dentist? Register(DentistCreateDto register)
        {
            if (_repo.UsernameExists(register.Username)) return null;
            if (_repo.EmailExists(register.Email)) return null;

            var hashPassword = HashPassword(register.Password);
            var dentist = new Dentist
            {
                Fullname = register.Fullname,
                Username = register.Username,
                Email = register.Email,
                PasswordHash = hashPassword,
                CreatedOn = DateTime.UtcNow
            };

            _repo.AddDentist(dentist);
            return dentist;
        }

        public Dentist? ValidateUser(DentistLoginDto loginDto)
        {
            var dentist = _repo.GetDentistByUsername(loginDto.Username);
            if (dentist == null) return null;

            if (VerifyPassword(loginDto.Password, dentist.PasswordHash))
                return dentist;

            return null;
        }

        public Dentist? UpdateDentist(int id, DentistUpdateDto update)
        {
            var dentist = _repo.GetDentistById(id);
            if (dentist == null) return null;

            dentist.Fullname = update.Fullname;
            if (!string.IsNullOrWhiteSpace(update.Password))
                dentist.PasswordHash = HashPassword(update.Password);

            _repo.EditDentist(dentist);
            return dentist;
        }

        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha.ComputeHash(bytes));
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
