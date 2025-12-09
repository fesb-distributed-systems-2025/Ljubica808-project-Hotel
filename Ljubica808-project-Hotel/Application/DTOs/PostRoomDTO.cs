using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PostRoomDTO
    {
        public long Id { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public double PricePerNight { get; set; }
        public RoomType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
