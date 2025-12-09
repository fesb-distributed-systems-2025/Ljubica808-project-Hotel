using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }

        public DateTime Checkin {  get; set; }
        public DateTime Checkout { get; set; }

        public double TotalPrice {  get; set; }

    }
}
