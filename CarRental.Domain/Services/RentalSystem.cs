using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Domain.Services
{
    public class RentalSystem(ICustomerChecker customerValidator)
    {
        private readonly ICustomerChecker _customerValidator = customerValidator;

        /// <summary>
        /// Creates a new Customer and returns it.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="fullName">Customer Name</param>
        /// <param name="address">Customer Address</param>
        /// <returns>The New Customer</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Customer> RegisterCustomerAsync(string id, string fullName, string address)
        {
            if (string.IsNullOrEmpty(fullName)) throw new ArgumentException("The Name provided is either null or empty", nameof(fullName));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("The Address provided is either null or empty", nameof(address));
            if (await _customerValidator.ExistsAsync(id)) throw new InvalidOperationException("Customer already exists");

            return new Customer
            {
                Id = Guid.NewGuid(),
                CustomerId = id,
                FullName = fullName,
                Address = address
            };
        }

        public bool CheckAvailability(Car car, DateOnly startDate, DateOnly endDate)
        {
            return car.IsAvailable(startDate, endDate);
        }

        /// <summary>
        /// Creates a new rental and returns it.
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="car">Car</param>
        /// <param name="startDate">Start Date for the Rental</param>
        /// <param name="endDate">End Date for the Rental</param>
        /// <returns>The New Rental</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Rental RegisterRental(Customer customer, Car car, DateOnly startDate, DateOnly endDate)
        {
            if (!car.IsAvailable(startDate, endDate)) throw new InvalidOperationException("The car is not available within the specified dates");

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                Car = car,
                Customer = customer,
                StartDate = startDate,
                EndDate = endDate,
                Status = Rental.RentalStatus.Valid
            };

            car.Rentals.Add(rental);

            return rental;
        }

        /// <summary>
        /// Modifies a Rental, allows for changing the Car and Dates
        /// </summary>
        /// <param name="rental">Rental to modify</param>
        /// <param name="car">Car</param>
        /// <param name="startDate">Start Date for the Rental</param>
        /// <param name="endDate">End Date for the Rental</param>
        /// <returns>The Modified Rental</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Rental ModifyReservation(Rental rental, Car car, DateOnly startDate, DateOnly endDate)
        {
            rental.Car.Rentals.Remove(rental); //Remove the rental from the old car itself, probably not needed but better safe than sorry.

            if (rental.Car.Equals(car)) car.Rentals.Remove(rental); //Same here, this important for doing the IsAvailable call below.

            if (!car.IsAvailable(startDate, endDate)) throw new InvalidOperationException("The car is not available within the specified dates");

            rental.Car = car;
            rental.StartDate = startDate;
            rental.EndDate = endDate;

            car.Rentals.Add(rental);

            return rental;
        }

        /// <summary>
        /// Cancels a Rental
        /// </summary>
        /// <param name="rental">The Rental to Cancel</param>
        /// <returns>Cancelled Rental</returns>
        public Rental CancelRental(Rental rental)
        {
            rental.Status = Rental.RentalStatus.Canceled;
            return rental;
        }

    }
}
