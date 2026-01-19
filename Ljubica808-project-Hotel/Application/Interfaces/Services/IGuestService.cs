using Application.Common;
using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IGuestService
    {
        Task<Result<IEnumerable<Guest?>>> GetAllGuests();
        Task<Result<Guest?>> GetGuestById(int id);
        Task<Result<object>> AddGuest(PostGuestDTO dto);
        Task<Result<object>> UpdateGuest(PutGuestDTO dto);
        Task<Result<object>> DeleteGuest(int id);
    }
}
