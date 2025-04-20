namespace CarRental.Infrastructure.Interfaces
{
    public interface IRentalRepository
    {
        Task<Domain.Entities.Rental?> GetAsync(Guid id);
        Task AddAsync(Domain.Entities.Rental entity);
        Task UpdateAsync(Domain.Entities.Rental entity);
        Task DeleteAsync(Domain.Entities.Rental entity);
    }
}
