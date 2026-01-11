using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth {  get; set; }
        public string? PhoneNumber {  get; set; }
        public string? Email { get; set; }

        // 1-N: jedan gost ima više rezervacija
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
