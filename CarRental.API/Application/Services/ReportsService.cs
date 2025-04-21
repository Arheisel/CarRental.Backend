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

        public async Task<IList<CarServiceDto>> GetCarsWithServicesAsync(DateOnly startDate, DateOnly endDate)
        {
            var cars = await _carRepository.GetCarsWithServicesAsync(startDate, endDate, ICarRepository.LoadOptions.FutureServices);

            return cars.Select(car => new CarServiceDto
            {
                Car = _mapper.Map<CarDto>(car),
                Date = car.Services.OrderBy(s => s.Date).First().Date
            })
            .ToList();
        }

        public async Task<IList<UtilizationByTypeReportDto>> GetRentalsByType(DateOnly startDate, DateOnly endDate)
        {
            var rentals = await _rentalRepository.GetAllBetweenDatesAsync(startDate, endDate);

            return rentals.GroupBy(r => r.Car.Type)
                .Select(group => new UtilizationByTypeReportDto
                {
                    Type = group.Key,
                    Percentage = (double)group.Count() / rentals.Count
                })
                .ToList();
        }
    }
}
