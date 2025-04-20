namespace CarRental.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Domain.Entities.Customer?> GetAsync(Guid id);
        Task AddAsync(Domain.Entities.Customer entity);
        Task UpdateAsync(Domain.Entities.Customer entity);
        Task DeleteAsync(Domain.Entities.Customer entity);
    }
}
