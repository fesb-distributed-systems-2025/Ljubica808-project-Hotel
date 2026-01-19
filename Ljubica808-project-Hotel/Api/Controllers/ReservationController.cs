using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var result = await _reservationService.GetReservationById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] PostReservationDTO reservation)
        {
            var result = await _reservationService.AddReservation(reservation);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation([FromBody] PutReservationDTO reservation)
        {
            var result = await _reservationService.UpdateReservation(reservation);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservation(id);
            return Ok(result);
        }
    }
}
