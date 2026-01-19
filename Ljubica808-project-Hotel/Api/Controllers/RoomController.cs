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
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var result = await _roomService.GetRoomById(id);
            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] PostRoomDTO room)
        {
           var result = await _roomService.AddRoom(room);
           return this.HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromBody] PutRoomDTO room)
        {
            var result = await _roomService.UpdateRoom(room);
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomService.DeleteRoom(id);
            return this.HandleResult(result);
        }
    }
}
