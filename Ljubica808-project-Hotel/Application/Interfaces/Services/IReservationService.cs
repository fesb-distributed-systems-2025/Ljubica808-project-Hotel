using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation?>> GetAllReservations();
        Task<Reservation?> GetReservationById(int id);
        Task<string> AddReservation(PostReservationDTO reservation);
        Task<string> UpdateReservation(PutReservationDTO reservation);
        Task<string> DeleteReservation(int id);
    }
}
