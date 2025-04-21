using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental?> GetAsync(Guid id);
        Task<IList<Rental>> GetAllForCustomerAsync(Guid customerId);
        Task<IList<Rental>> GetAllBetweenDatesAsync(DateOnly startDate, DateOnly endDate);
        Task AddAsync(Rental entity);
        Task UpdateAsync(Rental entity);
        Task DeleteAsync(Rental entity);
    }
}
