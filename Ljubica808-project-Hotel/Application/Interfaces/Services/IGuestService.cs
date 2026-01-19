using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IGuestService
    {
        Task<IEnumerable<Guest?>> GetAllGuests();
        Task<Guest?> GetGuestById(int id);
        Task<string> AddGuest(PostGuestDTO guest);
        Task<string> UpdateGuest(PutGuestDTO guest);
        Task<string> DeleteGuest(int id);
    }
}
