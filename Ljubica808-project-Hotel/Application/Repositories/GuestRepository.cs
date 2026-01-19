using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GuestRepository> _logger;

        public GuestRepository(
            IApplicationDbContext dbContext,
            ILogger<GuestRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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
            {
                _logger.LogWarning("Guest with id {GuestId} not found.", id);
                throw new KeyNotFoundException($"Guest {id} not found.");
            }

            return guest;
        }

        public void CreateGuest(Guest guest)
        {
            _dbContext.Guests.Add(guest);
        }

        public async Task UpdateGuest(Guest guest)
        {
            var oldGuest = await _dbContext.Guests.FindAsync(guest.Id);

            if (oldGuest == null)
            {
                _logger.LogWarning(
                    "Guest with id {GuestId} not found for update.",
                    guest.Id);

                throw new KeyNotFoundException($"Guest {guest.Id} not found.");
            }

            oldGuest.FirstName = guest.FirstName;
            oldGuest.LastName = guest.LastName;
            oldGuest.Email = guest.Email;
        }

        public async Task DeleteGuest(int id)
        {
            var guest = await _dbContext.Guests.FindAsync(id);

            if (guest == null)
            {
                _logger.LogWarning(
                    "Guest with id {GuestId} not found for delete.",
                    id);

                throw new KeyNotFoundException($"Guest {id} not found.");
            }

            _dbContext.Guests.Remove(guest);
        }
    }
}
