using CarRental.Domain.Entities;

namespace CarRental.Domain.Interfaces
{
    public interface IRentalSystem
    {
        Task<Customer> RegisterCustomerAsync(string id, string fullName, string address);
        bool CheckAvailability(Car car, DateOnly startDate, DateOnly endDate);
        Rental RegisterRental(Customer customer, Car car, DateOnly startDate, DateOnly endDate);
        Rental ModifyReservation(Rental rental, Car car, DateOnly startDate, DateOnly endDate);
        Rental CancelRental(Rental rental);
    }
}