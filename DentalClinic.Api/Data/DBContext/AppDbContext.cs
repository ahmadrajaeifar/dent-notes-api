using DentalClinic.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Api.Data.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Dentist> Dentists { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PatientService> PatientServices { get; set; } = null!;
        public DbSet<DentalServiceTariff> DentalServiceTariffs { get; set; } = null!;
    }
}
