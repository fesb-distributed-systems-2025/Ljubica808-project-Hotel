using Application.Common;
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

        public async Task<Result<IEnumerable<Room?>>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetRooms();
            return Result<IEnumerable<Room?>>.Success(rooms);
        }

        public async Task<Result<Room?>> GetRoomById(int id)
        {
            var room = await _roomRepository.GetRoomById(id);
            return Result<Room?>.Success(room);
        }

        public async Task<Result<object>> AddRoom(PostRoomDTO dto)
        {
            var room = dto.ToModel();
            var validation = ValidateRoom(room);

            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            _roomRepository.CreateRoom(room);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateRoom(PutRoomDTO dto)
        {
            var room = dto.ToModel();
            var validation = ValidateRoom(room);

            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            await _roomRepository.UpdateRoom(room);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteRoom(int id)
        {
            await _roomRepository.DeleteRoom(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        private ValidationResult ValidateRoom(Room room)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(room.RoomNumber))
                result.ValidationItems.Add("Room number is required.");

            if (room.Capacity <= 0)
                result.ValidationItems.Add("Capacity must be greater than zero.");

            if (room.PricePerNight < 0)
                result.ValidationItems.Add("Price per night cannot be negative.");

            if (!Enum.IsDefined(typeof(RoomType), room.Type))
                result.ValidationItems.Add("Room type is required.");

            return result;
        }
    }
}
