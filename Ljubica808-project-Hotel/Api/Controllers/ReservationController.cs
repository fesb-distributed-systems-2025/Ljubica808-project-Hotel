using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        // Koristimo fake database čisto da provjerimo je radi,
        // Implementirat ćemo svoju na sljedećim vježbama
        // Fake database (in-memory storage) 
        private static List<Reservation> _reservations = new();
        private static long _idCounter = 1;

        public ReservationController() { }

        [HttpGet("reservations")]
        public IEnumerable<Reservation> GetAllReservations() => _reservations;

        [HttpGet("reservations/{id}")]
        public ActionResult<Reservation> GetReservationById(long id)
        {
            var r = _reservations.FirstOrDefault(x => x.Id == id);
            return r == null ? NotFound("Reservation not found") : Ok(r);
        }

        [HttpPost("new")]
        public ActionResult<long> AddReservation([FromBody] PostReservationDTO dto)
        {
            int nights = (dto.Checkout - dto.Checkin).Days;
            if (nights < 1) nights = 1;

            var reservation = new Reservation
            {
                Id = (int)_idCounter++,
                RoomId = dto.RoomId,
                GuestId = dto.GuestId,
                Checkin = dto.Checkin,
                Checkout = dto.Checkout,
                TotalPrice = nights * 100 // temporary logic
            };

            _reservations.Add(reservation);
            return Ok(reservation.Id);
        }

        [HttpPut("edit")]
        public ActionResult<long> EditReservation([FromBody] Reservation updated)
        {
            var r = _reservations.FirstOrDefault(x => x.Id == updated.Id);
            if (r == null) return NotFound("Reservation not found");

            r.RoomId = updated.RoomId;
            r.GuestId = updated.GuestId;
            r.Checkin = updated.Checkin;
            r.Checkout = updated.Checkout;
            r.TotalPrice = updated.TotalPrice;

            return Ok(r.Id);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<long> DeleteReservation(long id)
        {
            var exists = _reservations.Any(x => x.Id == id);
            if (!exists) return NotFound("Reservation not found");

            _reservations = _reservations.Where(x => x.Id != id).ToList();
            return Ok(id);
        }
    }
}
