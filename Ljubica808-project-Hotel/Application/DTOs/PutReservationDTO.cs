using Domain.Models;

namespace Application.DTOs
{
    public class PutReservationDTO
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public int GuestId { get; set; }

        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }

        public double TotalPrice { get; set; }

        public Domain.Models.Reservation ToModel()
        {
            return new Domain.Models.Reservation
            {
                Id = Id,
                RoomId = RoomId,
                GuestId = GuestId,
                Checkin = Checkin,
                Checkout = Checkout,
                TotalPrice = TotalPrice
            };
        }
    }
}
