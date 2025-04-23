using CarRental.API.Application.DTOs;
using CarRental.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController(IRentalService rentalService) : ControllerBase
    {
        private readonly IRentalService _rentalService = rentalService;

        [HttpPost]
        [ProducesResponseType<RentalDto>(200)]
        [ProducesErrorResponseType(typeof(ErrorDto))]
        public async Task<IActionResult> RegisterRental([FromBody] AddRentalDto dto)
        {
            return Ok(await _rentalService.RegisterRentalAsync(dto));
        }

        [HttpPut("{id}")]
        [ProducesResponseType<RentalDto>(200)]
        public async Task<IActionResult> ModdifyReservation([FromRoute] Guid id, [FromBody] UpdateRentalDto dto)
        {
            return Ok(await _rentalService.ModifyReservationAsync(id, dto));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType<RentalDto>(200)]
        public async Task<IActionResult> CancelRental([FromRoute] Guid id)
        {
            return Ok(await _rentalService.CancelRentalAsync(id));
        }
    }
}
