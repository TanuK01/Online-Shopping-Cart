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
    public class ReservationsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ReservationsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public IActionResult GetAllReservations()
        {
            return Ok(_context.Reservations.ToList());
        }

        // GET: api/Reservations
        [HttpGet("{id}")]
        public IActionResult GetReservationByGuestId(int id)
        {
            var search = _context.Guests.Find(id);

            if (search == null)
            {
                return NotFound();
            }

            return Ok(search.Reservations.ToList());
        }

        // POST: api/Reservations
        [HttpPost]
        public IActionResult MakeReservation(int id, AddReservation _reservation)
        {
            var isAvailable = _context.Rooms.Find(id);

            if (isAvailable == null || isAvailable.roomStatus == false)
            {
                return NotFound();
            }

            var reservation = new Reservation();
            {
                reservation.reservationCheckIn = _reservation.reservationCheckIn;
                reservation.reservationCheckOut = _reservation.reservationCheckOut;
                reservation.reservationNoofGuests = _reservation.reservationNoofGuests;
            }

            _context.Reservations.Add(reservation);

            isAvailable.roomStatus = false;

            _context.SaveChanges();

            return Ok(_context.Reservations.ToList());
        }
    }
}
