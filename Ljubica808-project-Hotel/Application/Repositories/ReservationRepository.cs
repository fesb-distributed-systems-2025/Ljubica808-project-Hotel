using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ReservationRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _dbContext.Reservations
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation {id} not found.");

            return reservation;
        }

        public void CreateReservation(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
        }

        public async Task UpdateReservation(Reservation reservation)
        {
            var oldReservation = await _dbContext.Reservations.FindAsync(reservation.Id)
                ?? throw new KeyNotFoundException($"Reservation {reservation.Id} not found.");

            oldReservation.RoomId = reservation.RoomId;
            oldReservation.GuestId = reservation.GuestId;
            oldReservation.Checkin = reservation.Checkin;
            oldReservation.Checkout = reservation.Checkout;
            oldReservation.TotalPrice = reservation.TotalPrice;
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation {id} not found.");

            _dbContext.Reservations.Remove(reservation);
        }
    }
}
