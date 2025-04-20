using CarRental.Domain.Entities;
using CarRental.Infrastructure.Repositories;

namespace CarRental.Infrastructure.Interfaces
{
    public interface ICarRepository
    {
        Task<Car?> GetAsync(Guid id, LoadOptions options = LoadOptions.None);
        Task AddAsync(Car entity);
        Task UpdateAsync(Car entity);
        Task DeleteAsync(Car entity);

        [Flags]
        public enum LoadOptions
        {
            None = 0,
            AllServices = 1,
            FutureServices = 2,
            AllRentals = 4,
            FutureRentals = 8,
            Future = FutureServices | FutureRentals,
            All = AllServices | AllRentals
        }
    }
}
