using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.Entities;

namespace DentalClinic.Api.Repositories
{
    public class DentalServiceRepository
    {
        private readonly AppDbContext _context;
        public DentalServiceRepository(AppDbContext context) => _context = context;

        public DentalService AddDentalService(DentalService service)
        {
            _context.DentalServices.Add(service);
            _context.SaveChanges();
            return service;
        }

        public List<DentalService> GetAll() =>
            _context.DentalServices.Where(s => s.IsActive).ToList();

        public DentalService? GetById(int id) =>
            _context.DentalServices.FirstOrDefault(s => s.Id == id);
    }
}
