using Domain.Models;

namespace Application.DTOs
{
    public class PostReservationDTO
    {
        public int RoomId { get; set; }
        public int GuestId { get; set; }

        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }

        public double TotalPrice { get; set; }

        public Domain.Models.Reservation ToModel()
        {
            return new Domain.Models.Reservation
            {
                RoomId = RoomId,
                GuestId = GuestId,
                Checkin = Checkin,
                Checkout = Checkout,
                TotalPrice = TotalPrice
            };
        }
    }
}
