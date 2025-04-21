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

        [HttpGet("car/services")]
        [ProducesResponseType<IList<CarServiceDto>>(200)]
        public async Task<IActionResult> GetCarsWithServices([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            return Ok(await _reportsService.GetCarsWithServicesAsync(startDate, endDate));
        }

        [HttpGet("rentals/byCarType")]
        [ProducesResponseType<IList<CarServiceDto>>(200)]
        public async Task<IActionResult> GetRentalsByType([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            return Ok(await _reportsService.GetRentalsByType(startDate, endDate));
        }
    }
}
