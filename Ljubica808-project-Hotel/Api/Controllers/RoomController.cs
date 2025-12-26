using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        // Koristimo fake database čisto da provjerimo je radi,
        // Implementirat ćemo svoju na sljedećim vježbama
        // Fake database (in-memory storage) 
        private static List<Room> _rooms = new();
        private static long _idCounter = 1;

        public RoomController() { }

        [HttpGet("rooms")]
        public IEnumerable<Room> GetAllRooms() => _rooms;

        [HttpGet("rooms/{id}")]
        public ActionResult<Room> GetRoomById(long id)
        {
            var room = _rooms.FirstOrDefault(x => x.Id == id);
            return room == null ? NotFound("Room not found") : Ok(room);
        }

        [HttpPost("new")]
        public ActionResult<long> AddRoom([FromBody] PostRoomDTO dto)
        {
            var room = new Room
            {
                Id = (int)_idCounter++,
                RoomNumber = dto.RoomNumber,
                Floor = dto.Floor,
                Capacity = dto.Capacity,
                PricePerNight = dto.PricePerNight,
                Type = dto.Type,
                IsAvailable = dto.IsAvailable
            };

            _rooms.Add(room);
            return Ok(room.Id);
        }

        [HttpPut("edit")]
        public ActionResult<long> EditRoom([FromBody] Room updated)
        {
            var room = _rooms.FirstOrDefault(x => x.Id == updated.Id);
            if (room == null) return NotFound("Room not found");

            room.RoomNumber = updated.RoomNumber;
            room.Floor = updated.Floor;
            room.Capacity = updated.Capacity;
            room.PricePerNight = updated.PricePerNight;
            room.Type = updated.Type;
            room.IsAvailable = updated.IsAvailable;

            return Ok(room.Id);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<long> DeleteRoom(long id)
        {
            var exists = _rooms.Any(x => x.Id == id);
            if (!exists) return NotFound("Room not found");

            _rooms = _rooms.Where(x => x.Id != id).ToList();
            return Ok(id);
        }
    }
}
