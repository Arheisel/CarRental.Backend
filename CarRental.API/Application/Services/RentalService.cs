using AutoMapper;
using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using CarRental.API.Exceptions;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Interfaces;

namespace CarRental.API.Application.Services
{
    public class RentalService(IRentalSystem rentalSystem, ICustomerRepository customerRepository, IRentalRepository rentalRepository, ICarRepository carRepository, IMapper mapper) : IRentalService
    {
        private readonly IRentalSystem _rentalSystem = rentalSystem;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly ICarRepository _carRepository = carRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<string>> GetCarTypes()
        {
            return await _carRepository.GetTypesAsync();
        }

        public async Task<IList<CarDto>> GetAvailableCars(string type, DateOnly startDate, DateOnly endDate)
        {
            var cars = await _carRepository.GetAvailableAsync(type, startDate, endDate, ICarRepository.LoadOptions.None);

            return _mapper.Map<IList<CarDto>>(cars);
        }

        public async Task<IList<RentalDto>> GetRentalsAsync(string customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId)
                ?? throw new NotFoundException($"A customer with the ID {customerId} was not found");

            var rentals = await _rentalRepository.GetAllForCustomerAsync(customer.Id);

            return _mapper.Map<IList<RentalDto>>(rentals.OrderBy(r => r.StartDate));
        }

        public async Task RegisterRentalAsync(AddRentalDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustomerId)) throw new ApplicationException("Customer ID cannot be empty");
            if (string.IsNullOrWhiteSpace(dto.CustomerName)) throw new ApplicationException("Customer Name cannot be empty");
            if (string.IsNullOrWhiteSpace(dto.CustomerAddress)) throw new ApplicationException("Customer Address cannot be empty");

            var car = await _carRepository.GetAsync(dto.CarId, ICarRepository.LoadOptions.Future)
                ?? throw new NotFoundException($"The selected car was not found");

            var customer = await _customerRepository.GetAsync(dto.CustomerId);

            customer ??= await _rentalSystem.RegisterCustomerAsync(dto.CustomerId, dto.CustomerName, dto.CustomerAddress);

            var rental = _rentalSystem.RegisterRental(customer, car, dto.StartDate, dto.EndDate);

            await _rentalRepository.AddAsync(rental);
        }

        public async Task ModifyReservationAsync(Guid rentalId, UpdateRentalDto dto)
        {
            var rental = await _rentalRepository.GetAsync(rentalId)
                ?? throw new NotFoundException($"The reservation was not found");

            var car = await _carRepository.GetAsync(dto.CarId, ICarRepository.LoadOptions.Future)
                ?? throw new NotFoundException($"The selected car was not found");

            rental = _rentalSystem.ModifyReservation(rental, car, dto.StartDate, dto.EndDate);

            await _rentalRepository.UpdateAsync(rental);
        }

        public async Task CancelRentalAsync(Guid rentalId)
        {
            var rental = await _rentalRepository.GetAsync(rentalId)
                ?? throw new NotFoundException($"The reservation was not found");

            rental = _rentalSystem.CancelRental(rental);

            await _rentalRepository.UpdateAsync(rental);
        }
    }
}
