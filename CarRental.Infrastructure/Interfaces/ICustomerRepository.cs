using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetAsync(Guid id, LoadOptions options = LoadOptions.None);
        Task<Customer?> GetAsync(string customerId, LoadOptions options = LoadOptions.None);
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(Customer entity);

        [Flags]
        public enum LoadOptions
        {
            None = 0,
            Rentals = 1,
            All = Rentals
        }
    }
}
