using Application.Common;
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

        public async Task<Result<IEnumerable<Guest?>>> GetAllGuests()
        {
            var guests = await _guestRepository.GetGuests();
            return Result<IEnumerable<Guest?>>.Success(guests);
        }

        public async Task<Result<Guest?>> GetGuestById(int id)
        {
            var guest = await _guestRepository.GetGuestById(id);
            return Result<Guest?>.Success(guest);
        }

        public async Task<Result<object>> AddGuest(PostGuestDTO dto)
        {
            var guest = dto.ToModel();
            var validation = ValidateGuest(guest);

            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            _guestRepository.CreateGuest(guest);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateGuest(PutGuestDTO dto)
        {
            var guest = dto.ToModel();
            var validation = ValidateGuest(guest);

            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            await _guestRepository.UpdateGuest(guest);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteGuest(int id)
        {
            await _guestRepository.DeleteGuest(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        private ValidationResult ValidateGuest(Guest guest)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(guest.FirstName))
                result.ValidationItems.Add("First name is required.");
            else if (guest.FirstName.Length > Guest.FirstNameMaxLength)
                result.ValidationItems.Add($"First name cannot exceed {Guest.FirstNameMaxLength} characters.");

            if (string.IsNullOrWhiteSpace(guest.LastName))
                result.ValidationItems.Add("Last name is required.");
            else if (guest.LastName.Length > Guest.LastNameMaxLength)
                result.ValidationItems.Add($"Last name cannot exceed {Guest.LastNameMaxLength} characters.");

            if (guest.DateOfBirth >= DateTime.UtcNow)
                result.ValidationItems.Add("Date of birth must be in the past.");

            return result;
        }
    }
}
