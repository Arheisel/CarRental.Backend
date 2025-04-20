using AutoMapper;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CarRental.Infrastructure.Interfaces.ICarRepository;

namespace CarRental.Infrastructure.Repositories
{
    public class CarRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Car, Car>(context, mapper), ICarRepository
    {
        private IQueryable<Car> BuildGetQuery(LoadOptions options)
        {
            IQueryable<Car> query = _context.Cars.Include(c => c.Type);

            if (options.HasFlag(LoadOptions.AllServices))
            {
                query = query.Include(c => c.Services);
            }
            else if (options.HasFlag(LoadOptions.FutureServices))
            {
                query = query.Include(c => c.Services.Where(s => s.Date >= DateOnly.FromDateTime(DateTime.UtcNow)));
            }

            if (options.HasFlag(LoadOptions.AllRentals))
            {
                query = query.Include(c => c.Rentals);
            }
            else if (options.HasFlag(LoadOptions.FutureRentals))
            {
                query = query.Include(c => c.Rentals.Where(s => s.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow)))
                    .ThenInclude(r => r.Customer);
            }

            return query;
        }

        public async Task<Domain.Entities.Car?> GetAsync(Guid id, LoadOptions options = LoadOptions.None)
        {
            var car = await BuildGetQuery(options)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (car == null) return null;

            return _mapper.Map<Domain.Entities.Car>(car);
        }

        public override async Task AddAsync(Domain.Entities.Car car)
        {
            var type = await _context.CarTypes.FirstOrDefaultAsync(t => t.Name == car.Type)
                ?? throw new ApplicationException("Invalid Car Type");

            var e = _mapper.Map<Car>(car);
            e.TypeId = type.Id;
            e.DateAdded = DateTime.UtcNow;
            _context.Add(e);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Domain.Entities.Car car)
        {
            var type = await _context.CarTypes.FirstOrDefaultAsync(t => t.Name == car.Type)
                ?? throw new ApplicationException("Invalid Car Type");

            var e = _mapper.Map<Car>(car);
            e.TypeId = type.Id;
            e.DateModified = DateTime.UtcNow;
            _context.Update(e);
            await _context.SaveChangesAsync();
        }
    }
}
