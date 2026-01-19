using Api.Common;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGuests()
        {
            var result = await _guestService.GetAllGuests();
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestById(int id)
        {
            var result = await _guestService.GetGuestById(id);
            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddGuest([FromBody] PostGuestDTO guest)
        {
            var result = await _guestService.AddGuest(guest);
            return this.HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuest([FromBody] PutGuestDTO guest)
        {
            var result = await _guestService.UpdateGuest(guest);
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var result = await _guestService.DeleteGuest(id);
            return this.HandleResult(result);
        }
    }
}
