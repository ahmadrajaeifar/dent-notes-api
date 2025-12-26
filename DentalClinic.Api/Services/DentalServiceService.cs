using DentalClinic.Api.DTOs.Dental;
using DentalClinic.Api.Entities;
using DentalClinic.Api.Repositories;

namespace DentalClinic.Api.Services
{
    public class DentalServiceService
    {
        private readonly DentalServiceRepository _repo;
        public DentalServiceService(DentalServiceRepository repo) => _repo = repo;

        public DentalServiceReadDto Create(DentalServiceCreateDto dto)
        {
            var entity = new DentalService
            {
                Name = dto.Name,
                Description = dto.Description,
                Fee = dto.Fee
            };
            var created = _repo.AddDentalService(entity);
            return new DentalServiceReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Fee = created.Fee
            };
        }

        public List<DentalServiceReadDto> GetAll()
        {
            return _repo.GetAll().Select(s => new DentalServiceReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Fee = s.Fee
            }).ToList();
        }
    }
}
