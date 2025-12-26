using DentalClinic.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Api.Data.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Dentist> Dentists { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<DentalService> DentalServices { get; set; } = null!;
        public DbSet<PatientProcedure> PatientServices { get; set; } = null!;
        
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<DentalServiceTariff> DentalServiceTariffs { get; set; } = null!;
    }
}
