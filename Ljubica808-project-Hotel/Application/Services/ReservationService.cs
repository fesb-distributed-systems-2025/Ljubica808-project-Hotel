using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(
            IReservationRepository reservationRepository,
            IGuestRepository guestRepository,
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Reservation?>>> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetReservations();
            return Result<IEnumerable<Reservation?>>.Success(reservations);
        }

        public async Task<Result<Reservation?>> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);
            return Result<Reservation?>.Success(reservation);
        }

        public async Task<Result<object>> AddReservation(PostReservationDTO dto)
        {
            var reservation = dto.ToModel();
            var validation = await ValidateReservation(reservation);

            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            _reservationRepository.CreateReservation(reservation);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateReservation(PutReservationDTO dto)
        {
            var reservation = dto.ToModel();
            var validation = await ValidateReservation(reservation);

            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            await _reservationRepository.UpdateReservation(reservation);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteReservation(int id)
        {
            await _reservationRepository.DeleteReservation(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        private async Task<ValidationResult> ValidateReservation(Reservation reservation)
        {
            var result = new ValidationResult();

            var guest = await _guestRepository.GetGuestById(reservation.GuestId);
            if (guest is null)
                result.ValidationItems.Add("Guest not found.");

            var room = await _roomRepository.GetRoomById(reservation.RoomId);
            if (room is null)
                result.ValidationItems.Add("Room not found.");

            if (reservation.Checkin >= reservation.Checkout)
                result.ValidationItems.Add("Checkout date must be after checkin date.");

            if (reservation.TotalPrice <= 0)
                result.ValidationItems.Add("Total price cannot be negative or 0.");

            return result;
        }
    }
}
