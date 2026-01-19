using Domain.Models;

namespace Application.DTOs
{
    public class PutEmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EmployeePosition Position { get; set; }

        public Domain.Models.Employee ToModel()
        {
            return new Domain.Models.Employee
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Position = Position
            };
        }
    }
}
