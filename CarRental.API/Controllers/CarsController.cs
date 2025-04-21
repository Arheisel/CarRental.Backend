using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    public class CarsController(IRentalService rentalService) : ControllerBase
    {
        private readonly IRentalService _rentalService = rentalService;

        [HttpGet("types")]
        [ProducesResponseType<IList<string>>(200)]
        public async Task<IActionResult> GetTypes()
        {
            return Ok(await _rentalService.GetCarTypes());
        }

        [HttpGet("available")]
        [ProducesResponseType<IList<CarDto>>(200)]
        public async Task<IActionResult> GetAvailable([FromQuery] string type, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            return Ok(await _rentalService.GetAvailableCars(type, startDate, endDate));
        }
    }
}
