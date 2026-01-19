using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public GuestRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Guest>> GetGuests()
        {
            return await _dbContext.Guests
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Guest?> GetGuestById(int id)
        {
            var guest = await _dbContext.Guests.FindAsync(id);

            if (guest == null)
                throw new KeyNotFoundException($"Guest {id} not found.");

            return guest;
        }

        public void CreateGuest(Guest guest)
        {
            _dbContext.Guests.Add(guest);
        }

        public async Task UpdateGuest(Guest guest)
        {
            var oldGuest = await _dbContext.Guests.FindAsync(guest.Id)
                ?? throw new KeyNotFoundException($"Guest {guest.Id} not found.");

            oldGuest.FirstName = guest.FirstName;
            oldGuest.LastName = guest.LastName;
            oldGuest.DateOfBirth = guest.DateOfBirth;
            oldGuest.PhoneNumber = guest.PhoneNumber;
            oldGuest.Email = guest.Email;
        }

        public async Task DeleteGuest(int id)
        {
            var guest = await _dbContext.Guests.FindAsync(id);

            if (guest == null)
                throw new KeyNotFoundException($"Guest {id} not found.");

            _dbContext.Guests.Remove(guest);
        }
    }
}
