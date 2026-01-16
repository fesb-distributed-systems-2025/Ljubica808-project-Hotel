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
        private readonly IRoomRepository _roomRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(
            IReservationRepository reservationRepository,
            IRoomRepository roomRepository,
            IGuestRepository guestRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _guestRepository = guestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Reservation?>> GetAllReservations() => await _reservationRepository.GetReservations(); // Kraća verzija funkcije
        public async Task<Reservation?> GetReservationById(int id) => await _reservationRepository.GetReservationById(id);

        public async Task<string> AddReservation(PostReservationDTO dto)
        {
            var reservation = dto.ToModel();

            var validationResult = await ValidateReservation(reservation);
            if (validationResult != null)
                return validationResult;

            _reservationRepository.CreateReservation(reservation);
            await _unitOfWork.SaveChangesAsync();

            return $"Reservation successfully created with Id: {reservation.Id}";
        }

        public async Task<string> UpdateReservation(PutReservationDTO dto)
        {
            var reservation = dto.ToModel();

            var validationResult = await ValidateReservation(reservation);
            if (validationResult != null)
                return validationResult;

            await _reservationRepository.UpdateReservation(reservation);
            await _unitOfWork.SaveChangesAsync();

            return $"Reservation successfully updated with Id: {reservation.Id}";
        }

        public async Task<string> DeleteReservation(int id)
        {
            await _reservationRepository.DeleteReservation(id);
            await _unitOfWork.SaveChangesAsync();

            return $"Reservation successfully deleted with Id: {id}";
        }

        private async Task<string?> ValidateReservation(Reservation reservation)
        {
            var guest = await _guestRepository.GetGuestById(reservation.GuestId);
            if (guest == null)
                return "Guest not found.";

            var room = await _roomRepository.GetRoomById(reservation.RoomId);
            if (room == null)
                return "Room not found.";

            if (reservation.Checkin >= reservation.Checkout)
                return "Checkin must be before Checkout.";

            if (reservation.TotalPrice <= 0)
                return "Total price must be greater than zero.";

            return null;
        }
    }
}
