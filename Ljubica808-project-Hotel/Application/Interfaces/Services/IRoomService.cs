using Application.Common;
using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Result<IEnumerable<Room?>>> GetAllRooms();
        Task<Result<Room?>> GetRoomById(int id);
        Task<Result<object>> AddRoom(PostRoomDTO dto);
        Task<Result<object>> UpdateRoom(PutRoomDTO dto);
        Task<Result<object>> DeleteRoom(int id);
    }
}
