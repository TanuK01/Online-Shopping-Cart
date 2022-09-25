using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementApp.Models;

namespace HotelManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public GuestsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Guests
        [HttpGet]
        public IActionResult GetAllGuest()
        {
            return Ok(_context.Guests.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetUniqueGuest(int id)
        {
            var search = _context.Guests.Find(id);

            if (search == null)
            {
                return NotFound();
            }

            return Ok(search);
        }
        // POST: api/Guests
        [HttpPost]
        public IActionResult Post(Guest guest)
        {
            if (guest.Reservations == null)
                guest.Reservations = new List<Reservation>();

            _context.Guests.Add(guest);
            _context.SaveChanges();

            return Ok(_context.Guests.ToList());
        }

        // PUT: api/Guests
        [HttpPut("{id}")]
        public IActionResult Put(int id, Guest _guest)
        {
            var guest = _context.Guests.Find(id);

            if (guest == null)
            {
                return NotFound();
            }

            guest = _guest;

            return Ok(_context.SaveChanges());
        }
    }
}
