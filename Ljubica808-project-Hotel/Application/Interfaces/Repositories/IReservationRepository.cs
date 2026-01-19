using Domain.Models;

namespace Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation?> GetReservationById(int id);
        void CreateReservation(Reservation reservation);
        Task UpdateReservation(Reservation reservation);
        Task DeleteReservation(int id);
    }
}
