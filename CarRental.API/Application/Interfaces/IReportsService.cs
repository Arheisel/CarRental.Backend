using CarRental.API.Application.DTOs;

namespace CarRental.API.Application.Interfaces
{
    public interface IReportsService
    {
        Task<IList<CarServiceDto>> GetCarsWithServicesAsync(DateOnly startDate, DateOnly endDate);
        Task<IList<UtilizationByTypeReportDto>> GetRentalsByType(DateOnly startDate, DateOnly endDate);
    }
}