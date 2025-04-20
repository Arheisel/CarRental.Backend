using AutoMapper;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class CarRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Car, Car>(context, mapper), ICarRepository
    {
        public async Task<Domain.Entities.Car?> GetAsync(Guid id)
        {
            var car = await _context.Cars
                .Include(c => c.Services)
                .Include(c => c.Rentals)
                    .ThenInclude(r => r.Customer)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (car == null) return null;

            return _mapper.Map<Domain.Entities.Car>(car);
        }
    }

    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Domain.Entities.CarService, CarService>();
            CreateMap<CarService, Domain.Entities.CarService>();

            CreateMap<Domain.Entities.Car, Car>()
                .AfterMap((src, dest) =>
                {
                    foreach (var service in dest.Services) service.CarId = dest.Id;
                });


            CreateMap<Car, Domain.Entities.Car>();
        }
    }
}
