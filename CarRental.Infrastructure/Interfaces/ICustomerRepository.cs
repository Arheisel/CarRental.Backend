using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetAsync(Guid id);
        Task<Customer?> GetAsync(string customerId);
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(Customer entity);
    }
}
