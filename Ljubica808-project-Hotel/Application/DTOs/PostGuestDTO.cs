using Domain.Models;

namespace Application.DTOs
{
    public class PostGuestDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public Domain.Models.Guest ToModel()
        {
            return new Domain.Models.Guest
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
        }
    }
}
