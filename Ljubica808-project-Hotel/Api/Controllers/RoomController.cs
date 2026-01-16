using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var result = await _roomService.GetRoomById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] PostRoomDTO room)
        {
            var result = await _roomService.AddRoom(room);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromBody] PutRoomDTO room)
        {
            var result = await _roomService.UpdateRoom(room);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomService.DeleteRoom(id);
            return Ok(result);
        }
    }
}
