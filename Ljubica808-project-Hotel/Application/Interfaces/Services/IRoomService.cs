using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room?>> GetAllRooms();
        Task<Room?> GetRoomById(int id);
        Task<string> AddRoom(PostRoomDTO room);
        Task<string> UpdateRoom(PutRoomDTO room);
        Task<string> DeleteRoom(int id);
    }
}
