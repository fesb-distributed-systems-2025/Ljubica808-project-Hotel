using Application.Common;
using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Result<IEnumerable<Reservation?>>> GetAllReservations();
        Task<Result<Reservation?>> GetReservationById(int id);
        Task<Result<object>> AddReservation(PostReservationDTO dto);
        Task<Result<object>> UpdateReservation(PutReservationDTO dto);
        Task<Result<object>> DeleteReservation(int id);
    }
}
