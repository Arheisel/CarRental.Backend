using AutoMapper;
using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using CarRental.Infrastructure.Interfaces;

namespace CarRental.API.Application.Services
{
    public class ReportsService(ICarRepository carRepository, IRentalRepository rentalRepository, IMapper mapper) : IReportsService
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<CarServiceReportDto>> GetCarsWithServicesAsync(DateOnly startDate, DateOnly endDate)
        {
            if (startDate == default) throw new ApplicationException("Invalid start date");
            if (endDate == default) throw new ApplicationException("Invalid end date");

            var cars = await _carRepository.GetCarsWithServicesAsync(startDate, endDate, ICarRepository.LoadOptions.FutureServices);

            return cars.Select(car => new CarServiceReportDto
            {
                Car = _mapper.Map<CarDto>(car),
                Date = car.Services.OrderBy(s => s.Date).First().Date
            })
            .ToList();
        }

        public async Task<IList<UtilizationByTypeReportDto>> GetRentalsByType(DateOnly startDate, DateOnly endDate)
        {
            if (startDate == default) throw new ApplicationException("Invalid start date");
            if (endDate == default) throw new ApplicationException("Invalid end date");

            var rentals = await _rentalRepository.GetAllBetweenDatesAsync(startDate, endDate);

            return rentals.Where(r => r.Status != Domain.Entities.Rental.RentalStatus.Canceled).GroupBy(r => r.Car.Type)
                .Select(group => new UtilizationByTypeReportDto
                {
                    Type = group.Key,
                    Percentage = (double)group.Count() / rentals.Count
                })
                .ToList();
        }
    }
}
