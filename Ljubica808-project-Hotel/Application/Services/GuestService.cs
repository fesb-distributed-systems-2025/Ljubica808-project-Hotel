using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;

namespace Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GuestService(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
        {
            _guestRepository = guestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Guest?>> GetAllGuests()
        {
            return await _guestRepository.GetGuests();
        }

        public async Task<Guest?> GetGuestById(int id)
        {
            return await _guestRepository.GetGuestById(id);
        }

        public async Task<string> AddGuest(PostGuestDTO guestDto)
        {
            var guest = guestDto.ToModel();

            var validationResult = ValidateGuest(guest);
            if (validationResult != null)
                return validationResult;

            _guestRepository.CreateGuest(guest);
            await _unitOfWork.SaveChangesAsync();

            return $"Guest successfully created with Id: {guest.Id}";
        }

        public async Task<string> UpdateGuest(PutGuestDTO guestDto)
        {
            var guest = guestDto.ToModel();

            var validationResult = ValidateGuest(guest);
            if (validationResult != null)
                return validationResult;

            await _guestRepository.UpdateGuest(guest);
            await _unitOfWork.SaveChangesAsync();

            return $"Guest successfully updated with Id: {guest.Id}";
        }

        public async Task<string> DeleteGuest(int id)
        {
            await _guestRepository.DeleteGuest(id);
            await _unitOfWork.SaveChangesAsync();

            return $"Guest successfully deleted with Id: {id}";
        }

        // Private validation method
        private string? ValidateGuest(Guest guest)
        {
            if (string.IsNullOrWhiteSpace(guest.FirstName))
                return "First name is required.";

            if (string.IsNullOrWhiteSpace(guest.LastName))
                return "Last name is required.";

            if (guest.DateOfBirth == default)
                return "Date of birth is required.";

            if (guest.DateOfBirth > DateTime.Now)
                return "Date of birth cannot be in the future.";

            return null;
        }
    }
}
