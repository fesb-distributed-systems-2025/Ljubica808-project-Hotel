using Domain.Models;

namespace Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRooms();
        Task<Room?> GetRoomById(int id);
        void CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
    }
}
