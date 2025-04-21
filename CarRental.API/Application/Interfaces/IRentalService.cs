using CarRental.API.Application.DTOs;

namespace CarRental.API.Application.Interfaces
{
    public interface IRentalService
    {
        Task<IList<string>> GetCarTypes();
        Task<IList<CarDto>> GetAvailableCars(string type, DateOnly startDate, DateOnly endDate);
        Task<IList<RentalDto>> GetRentalsAsync(string customerId);
        Task RegisterRentalAsync(AddRentalDto dto);
        Task ModifyReservationAsync(Guid rentalId, UpdateRentalDto dto);
        Task CancelRentalAsync(Guid rentalId);
    }
}