using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        // Foreign Keys
        public int RoomId { get; set; }
        public int GuestId { get; set; }


        public DateTime Checkin {  get; set; }
        public DateTime Checkout { get; set; }

        public double TotalPrice {  get; set; }

        // Navigation properties
        public Room Room { get; set; } = null!;
        public Guest Guest { get; set; } = null!;
    }
}
