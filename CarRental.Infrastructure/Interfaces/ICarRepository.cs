namespace CarRental.Infrastructure.Interfaces
{
    public interface ICarRepository
    {
        Task<Domain.Entities.Car?> GetAsync(Guid id);
        Task AddAsync(Domain.Entities.Car entity);
        Task UpdateAsync(Domain.Entities.Car entity);
        Task DeleteAsync(Domain.Entities.Car entity);
    }
}
