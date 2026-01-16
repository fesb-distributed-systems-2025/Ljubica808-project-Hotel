using Domain.Models;

namespace Application.DTOs
{
    public class PostRoomDTO
    {
        public string? RoomNumber { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public double PricePerNight { get; set; }
        public RoomType Type { get; set; }
        public bool IsAvailable { get; set; }

        public Domain.Models.Room ToModel()
        {
            return new Domain.Models.Room
            {
                RoomNumber = RoomNumber,
                Floor = Floor,
                Capacity = Capacity,
                PricePerNight = PricePerNight,
                Type = Type,
                IsAvailable = IsAvailable
            };
        }
    }
}
