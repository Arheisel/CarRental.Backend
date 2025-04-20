using CarRental.API.Application.DTOs;
using CarRental.API.Exceptions;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Interfaces;

namespace CarRental.API.Application.Services
{
    public class RentalService(IRentalSystem rentalSystem, ICustomerRepository customerRepository, IRentalRepository rentalRepository, ICarRepository carRepository)
    {
        private readonly IRentalSystem _rentalSystem = rentalSystem;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly ICarRepository _carRepository = carRepository;

        public async Task<RentalDto> GetRentalsAsync(string customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);

            if (customer == null) throw new NotFoundException($"A customer with the ID {customerId} was not found");


        }
    }
}
