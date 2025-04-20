using AutoMapper;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class RentalRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Rental, Rental>(context, mapper), IRentalRepository
    {
        public async Task<Domain.Entities.Rental?> GetAsync(Guid id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (rental == null) return null;

            return _mapper.Map<Domain.Entities.Rental>(rental);
        }
    }
}
