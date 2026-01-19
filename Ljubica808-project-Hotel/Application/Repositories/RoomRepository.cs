using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public RoomRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                throw new KeyNotFoundException($"Room {id} not found.");

            return room;
        }

        public void CreateRoom(Room room)
        {
            _dbContext.Rooms.Add(room);
        }

        public async Task UpdateRoom(Room room)
        {
            var oldRoom = await _dbContext.Rooms.FindAsync(room.Id)
                ?? throw new KeyNotFoundException($"Room {room.Id} not found.");

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
                throw new KeyNotFoundException($"Room {id} not found.");

            _dbContext.Rooms.Remove(room);
        }
    }
}
