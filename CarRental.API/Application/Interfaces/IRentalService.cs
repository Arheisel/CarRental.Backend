using CarRental.API.Application.DTOs;

namespace CarRental.API.Application.Interfaces
{
    public interface IRentalService
    {
        Task<IList<string>> GetCarTypes();
        Task<IList<CarDto>> GetAvailableCars(string type, DateOnly startDate, DateOnly endDate);
        Task<bool> CheckAvailability(Guid carId, DateOnly startDate, DateOnly endDate, Guid? rentalId = null);
        Task<IList<RentalDto>> GetRentalsAsync(string customerId);
        Task<RentalDto> RegisterRentalAsync(AddRentalDto dto);
        Task<RentalDto> ModifyReservationAsync(Guid rentalId, UpdateRentalDto dto);
        Task<RentalDto> CancelRentalAsync(Guid rentalId);
    }
}