using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Room?>> GetAllRooms() => await _roomRepository.GetRooms();
        public async Task<Room?> GetRoomById(int id) => await _roomRepository.GetRoomById(id);

        public async Task<string> AddRoom(PostRoomDTO dto)
        {
            var room = dto.ToModel();

            var validationResult = ValidateRoom(room);
            if (validationResult != null)
                return validationResult;

            _roomRepository.CreateRoom(room);
            await _unitOfWork.SaveChangesAsync();

            return $"Room successfully created with Id: {room.Id}";
        }

        public async Task<string> UpdateRoom(PutRoomDTO dto)
        {
            var room = dto.ToModel();

            var validationResult = ValidateRoom(room);
            if (validationResult != null)
                return validationResult;

            await _roomRepository.UpdateRoom(room);
            await _unitOfWork.SaveChangesAsync();

            return $"Room successfully updated with Id: {room.Id}";
        }

        public async Task<string> DeleteRoom(int id)
        {
            await _roomRepository.DeleteRoom(id);
            await _unitOfWork.SaveChangesAsync();

            return $"Room successfully deleted with Id: {id}";
        }

        private string? ValidateRoom(Room room)
        {
            if (string.IsNullOrWhiteSpace(room.RoomNumber))
                return "Room number is required.";

            if (room.Capacity <= 0)
                return "Capacity must be greater than zero.";

            if (room.PricePerNight <= 0)
                return "Price per night must be greater than zero.";

            return null;
        }
    }
}
