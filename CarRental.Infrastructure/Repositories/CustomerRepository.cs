using AutoMapper;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class CustomerRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Customer, Customer>(context, mapper), ICustomerRepository, ICustomerChecker
    {
        public async Task<Domain.Entities.Customer?> GetAsync(Guid id)
        {
            var customer = await _context.Customers
                .Include(c => c.Rentals)
                    .ThenInclude(r => r.Car)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null) return null;

            return _mapper.Map<Domain.Entities.Customer>(customer);
        }

        public Task<bool> ExistsAsync(string customerId)
        {
            return _context.Customers.AnyAsync(c => c.CustomerId == customerId);
        }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Entities.Customer, Customer>();
            CreateMap<Customer, Domain.Entities.Customer>();
        }
    }
}
