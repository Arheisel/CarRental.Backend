using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    public class ReportsController(IReportsService reportsService) : ControllerBase
    {
        private readonly IReportsService _reportsService = reportsService;

        [HttpGet("cars/services")]
        [ProducesResponseType<IList<CarServiceReportDto>>(200)]
        public async Task<IActionResult> GetCarsWithServices([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _reportsService.GetCarsWithServicesAsync(DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)));
        }

        [HttpGet("rentals/byCarType")]
        [ProducesResponseType<IList<UtilizationByTypeReportDto>>(200)]
        public async Task<IActionResult> GetRentalsByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _reportsService.GetRentalsByType(DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)));
        }
    }
}
