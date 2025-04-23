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

        [HttpGet("search")]
        [ProducesResponseType<IList<CarDto>>(200)]
        public async Task<IActionResult> GetAvailable([FromQuery] string type, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _rentalService.GetAvailableCars(type, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)));
        }

        [HttpGet("{id}/isAvailable")]
        [ProducesResponseType<bool>(200)]
        public async Task<IActionResult> GetAvailable([FromRoute] Guid id, [FromQuery] Guid? rentalId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _rentalService.CheckAvailability(id, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate), rentalId));
        }
    }
}
