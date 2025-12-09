using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {   
        // Koristimo fake database čisto da provjerimo je radi,
        // Implementirat ćemo svoju na sljedećim vježbama
        // Fake database (in-memory storage) 
        private static List<Guest> _guests = new();
        private static long _idCounter = 1;


        //ActionResult<tip podatka>- Može vratiti samo 404 NotFound() ili 200OK(Objekt postoji)
        // plus <tip podatka>
        public GuestController()
        {
        }

        // READ ALL: GET api/guest/guests
        [HttpGet("guests")]
        public IEnumerable<Guest> GetAllGuests()
        {
            return _guests;
        }
        // READ ONE: GET api/guest/guests/{id}
        [HttpGet("guests/{id}")]
        public ActionResult<Guest> GetGuestById([FromRoute] long id)
        {
            var guest = _guests.FirstOrDefault(x => x.Id == id);

            if (guest == null)
                return NotFound("Guest not found!");

            return Ok(guest);
        }


        // CREATE: POST api/guest/new
        [HttpPost("new")]
        public ActionResult<int> AddGuest([FromBody] PostGuestDTO newGuest)
        {
            var guest = new Guest
            {
                Id = _idCounter++,
                FirstName = newGuest.FirstName,
                LastName = newGuest.LastName,
                PhoneNumber = newGuest.PhoneNumber,
                Email = newGuest.Email,
                DateOfBirth = newGuest.DateOfBirth
            };

            _guests.Add(guest);

            return Ok(guest.Id);
        }

        // UPDATE: PUT api/guest/edit
        [HttpPut("edit")]
        public ActionResult<int> EditGuest([FromBody] Guest updatedGuest)
        {
            var oldGuest = _guests.FirstOrDefault(x => x.Id == updatedGuest.Id);

            if (oldGuest == null)
                return NotFound("Guest not found!");

            oldGuest.FirstName = updatedGuest.FirstName;
            oldGuest.LastName = updatedGuest.LastName;
            oldGuest.Email = updatedGuest.Email;
            oldGuest.PhoneNumber = updatedGuest.PhoneNumber;
            oldGuest.DateOfBirth = updatedGuest.DateOfBirth;

            return Ok(oldGuest.Id);
        }
        // DELETE: DELETE api/guest/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult<int> DeleteGuest([FromRoute] long id)
        {
            var exists = _guests.Any(x => x.Id == id);

            if (!exists)
                return NotFound("Guest not found!");

            _guests = _guests.Where(x => x.Id != id).ToList();

            return Ok((int)id);
        }
    }
}
