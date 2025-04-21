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
                var date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-Domain.Entities.Car.ServiceDurationDays);
                query = query.Include(c => c.Services.Where(s => s.Date >= date));
            }

            if (options.HasFlag(LoadOptions.AllRentals))
            {
                query = query.Include(c => c.Rentals);
            }
            else if (options.HasFlag(LoadOptions.FutureRentals))
            {
                var date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-Domain.Entities.Car.RentalEndDateOffset);
                query = query.Include(c => c.Rentals.Where(s => s.EndDate >= date))
                    .ThenInclude(r => r.Customer);
            }

            return query;
        }

        public async Task<IList<Domain.Entities.Car>> GetAllAsync(LoadOptions options = LoadOptions.None)
        {
            var cars = await BuildGetQuery(options).ToListAsync();

            return _mapper.Map<IList<Domain.Entities.Car>>(cars);
        }

        public async Task<IList<Domain.Entities.Car>> GetAllAsync(string type, LoadOptions options = LoadOptions.None)
        {
            var cars = await BuildGetQuery(options)
                .Where(c => c.Type!.Name == type)
                .ToListAsync();

            return _mapper.Map<IList<Domain.Entities.Car>>(cars);
        }

        public async Task<IList<Domain.Entities.Car>> GetAvailableAsync(string type, DateOnly startDate, DateOnly endDate, LoadOptions options = LoadOptions.None)
        {
            var rentalStartDate = startDate.AddDays(-Domain.Entities.Car.RentalEndDateOffset);
            var rentalEndDate = endDate.AddDays(Domain.Entities.Car.RentalEndDateOffset);
            var serviceStartDate = startDate.AddDays(-Domain.Entities.Car.ServiceDurationDays);
            var serviceEndDate = endDate.AddDays(Domain.Entities.Car.RentalEndDateOffset);
            var canceledStatusName = Enum.GetName(typeof(Domain.Entities.Rental.RentalStatus), Domain.Entities.Rental.RentalStatus.Canceled);

            var cars = await BuildGetQuery(options)
                .Where(c => c.Type!.Name == type)
                .Where(c => !c.Rentals.Any(r => r.Status != canceledStatusName && rentalStartDate <= r.EndDate && rentalEndDate >= r.StartDate))
                .Where(c => !c.Services.Any(s => serviceStartDate <= s.Date && serviceEndDate >= s.Date))
                .ToListAsync();

            return _mapper.Map<IList<Domain.Entities.Car>>(cars);
        }

        public async Task<IList<Domain.Entities.Car>> GetCarsWithServicesAsync(DateOnly startDate, DateOnly endDate, LoadOptions options = LoadOptions.None)
        {
            startDate = startDate.AddDays(-Domain.Entities.Car.ServiceDurationDays);

            var cars = await BuildGetQuery(options)
                .Where(c => c.Services.Any(s => startDate <= s.Date && endDate >= s.Date))
                .ToListAsync();

            return _mapper.Map<IList<Domain.Entities.Car>>(cars);
        }

        public async Task<Domain.Entities.Car?> GetAsync(Guid id, LoadOptions options = LoadOptions.None)
        {
            var car = await BuildGetQuery(options)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (car == null) return null;

            return _mapper.Map<Domain.Entities.Car>(car);
        }

        public async Task<IList<string>> GetTypesAsync()
        {
            return await _context.CarTypes.Select(c => c.Name).ToListAsync();
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
