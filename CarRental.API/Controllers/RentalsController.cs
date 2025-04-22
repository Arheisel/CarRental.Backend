using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    public class RentalsController(IRentalService rentalService) : ControllerBase
    {
        private readonly IRentalService _rentalService = rentalService;

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RegisterRental([FromBody] AddRentalDto dto)
        {
            await _rentalService.RegisterRentalAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ModdifyReservation([FromRoute] Guid id, [FromBody] UpdateRentalDto dto)
        {
            await _rentalService.ModifyReservationAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CancelRental([FromRoute] Guid id)
        {
            await _rentalService.CancelRentalAsync(id);
            return Ok();
        }
    }
}
