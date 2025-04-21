using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    public class CustomersController(IRentalService rentalService) : ControllerBase
    {
        private readonly IRentalService _rentalService = rentalService;

        [HttpGet("{customerId}/rentals")]
        [ProducesResponseType<IList<RentalDto>>(200)]
        public async Task<IActionResult> GetRentals([FromRoute] string customerId)
        {
            return Ok(await _rentalService.GetRentalsAsync(customerId));
        }
    }
}
