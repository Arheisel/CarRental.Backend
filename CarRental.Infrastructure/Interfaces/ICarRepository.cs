using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Interfaces
{
    public interface ICarRepository
    {
        Task<IList<Car>> GetAllAsync(LoadOptions options = LoadOptions.None);
        Task<IList<Car>> GetAllAsync(string type, LoadOptions options = LoadOptions.None);
        Task<IList<Car>> GetAvailableAsync(string type, DateOnly startDate, DateOnly endDate, LoadOptions options = LoadOptions.None);
        Task<IList<Car>> GetCarsWithServicesAsync(DateOnly startDate, DateOnly endDate, LoadOptions options = LoadOptions.None);
        Task<Car?> GetAsync(Guid id, LoadOptions options = LoadOptions.None);
        Task<IList<string>> GetTypesAsync();
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
