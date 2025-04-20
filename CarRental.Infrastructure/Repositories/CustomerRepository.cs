using AutoMapper;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CarRental.Infrastructure.Interfaces.ICustomerRepository;

namespace CarRental.Infrastructure.Repositories
{
    public class CustomerRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Customer, Customer>(context, mapper), ICustomerRepository, ICustomerChecker
    {
        private IQueryable<Customer> BuildGetQuery(LoadOptions options)
        {
            IQueryable <Customer> query = _context.Customers;

            if (options.HasFlag(LoadOptions.Rentals))
            {
                query = query.Include(c => c.Rentals)
                    .ThenInclude(r => r.Car);
            }

            return query;
        }

        public async Task<Domain.Entities.Customer?> GetAsync(Guid id, LoadOptions options = LoadOptions.None)
        {
            var customer = await BuildGetQuery(options)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null) return null;

            return _mapper.Map<Domain.Entities.Customer>(customer);
        }

        public async Task<Domain.Entities.Customer?> GetAsync(string customerId, LoadOptions options = LoadOptions.None)
        {
            var customer = await BuildGetQuery(options)
                .SingleOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null) return null;

            return _mapper.Map<Domain.Entities.Customer>(customer);
        }

        public Task<bool> ExistsAsync(string customerId)
        {
            return _context.Customers.AnyAsync(c => c.CustomerId == customerId);
        }
    }
}
