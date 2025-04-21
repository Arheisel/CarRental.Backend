using AutoMapper;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class RentalRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Rental, Rental>(context, mapper), IRentalRepository
    {
        private IQueryable<Rental> BuildGetQuery()
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Car);
        }

        public async Task<Domain.Entities.Rental?> GetAsync(Guid id)
        {
            var rental = await BuildGetQuery()
                .SingleOrDefaultAsync(r => r.Id == id);

            if (rental == null) return null;

            return _mapper.Map<Domain.Entities.Rental>(rental);
        }

        public async Task<IList<Domain.Entities.Rental>> GetAllForCustomerAsync(Guid customerId)
        {
            var rentals = await BuildGetQuery()
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

            return _mapper.Map<IList<Domain.Entities.Rental>>(rentals);
        }

        public async Task<IList<Domain.Entities.Rental>> GetAllBetweenDatesAsync(DateOnly startDate, DateOnly endDate)
        {
            var rentals = await BuildGetQuery()
                .Where(r => r.StartDate >= startDate && r.StartDate <= endDate)
                .ToListAsync();

            return _mapper.Map<IList<Domain.Entities.Rental>>(rentals);
        }
    }
}
