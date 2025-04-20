using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental?> GetAsync(Guid id);
        Task AddAsync(Rental entity);
        Task UpdateAsync(Rental entity);
        Task DeleteAsync(Rental entity);
    }
}
