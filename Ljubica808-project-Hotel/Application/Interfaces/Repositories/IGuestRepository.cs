using Domain.Models;

namespace Application.Interfaces.Repositories
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetGuests();
        Task<Guest?> GetGuestById(int id);
        void CreateGuest(Guest guest);
        Task UpdateGuest(Guest guest);
        Task DeleteGuest(int id);
    }
}
