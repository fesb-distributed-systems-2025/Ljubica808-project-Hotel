using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(
            IApplicationDbContext dbContext,
            ILogger<RoomRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _dbContext.Rooms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Room?> GetRoomById(int id)
        {
            var room = await _dbContext.Rooms.FindAsync(id);

            if (room == null)
            {
                _logger.LogWarning(
                    "Room with id {RoomId} not found.",
                    id);

                throw new KeyNotFoundException($"Room {id} not found.");
            }

            return room;
        }

        public void CreateRoom(Room room)
        {
            _dbContext.Rooms.Add(room);
        }

        public async Task UpdateRoom(Room room)
        {
            var oldRoom = await _dbContext.Rooms.FindAsync(room.Id);

            if (oldRoom == null)
            {
                _logger.LogWarning(
                    "Room with id {RoomId} not found for update.",
                    room.Id);

                throw new KeyNotFoundException($"Room {room.Id} not found.");
            }

            oldRoom.RoomNumber = room.RoomNumber;
            oldRoom.Floor = room.Floor;
            oldRoom.Capacity = room.Capacity;
            oldRoom.PricePerNight = room.PricePerNight;
            oldRoom.Type = room.Type;
            oldRoom.IsAvailable = room.IsAvailable;
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _dbContext.Rooms.FindAsync(id);

            if (room == null)
            {
                _logger.LogWarning(
                    "Room with id {RoomId} not found for delete.",
                    id);

                throw new KeyNotFoundException($"Room {id} not found.");
            }

            _dbContext.Rooms.Remove(room);
        }
    }
}
