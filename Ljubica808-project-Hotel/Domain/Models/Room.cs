using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Room
    {
        public long Id { get; set; }
        public string RoomNumber {  get; set; }
        public int Floor {  get; set; }
        public int Capacity {  get; set; }
        public double PricePerNight {  get; set; }
        public RoomType Type { get; set; }
        public bool IsActive { get; set; }
    }
    public enum RoomType // Jednostavije ovako implementirat nego da se koristi string
    {
        Single,
        Double,
        Triple,
        Suite,
        Deluxe
    }
}
