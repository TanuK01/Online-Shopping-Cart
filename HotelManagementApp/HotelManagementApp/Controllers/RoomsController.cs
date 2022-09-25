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
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            return Ok(_context.Rooms.ToList());
        }

        // GET: api/Rooms
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // POST: api/Rooms
        [HttpPost]
        public IActionResult AddRoom(AddRoom _room)
        {
            var room = new Room();
            {
                room.roomStatus = _room.roomStatus;
                room.roomComment = _room.roomComment;
                room.roomPrice = _room.roomPrice;
                room.roomInventory = _room.roomInventory;
            }

            _context.Rooms.Add(room);
            return Ok(_context.SaveChanges());
        }

        // PUT: api/Rooms
        [HttpPut("/UpdateRoomStatus/{id}")]
        public IActionResult UpdateRoomStatus(int id, UpdateRoom _room)
        {
            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }

            room.roomStatus = _room.roomStatus;
            return Ok(_context.SaveChanges());
        }

        //PUT: api/Rooms
        [HttpPut("/UpdateRoomInventory/{id}")]
        public IActionResult UpdateRoomInventory(int id, UpdateRoom _room)
        {
            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }

            room.roomInventory = _room.roomInventory;
            return Ok(_context.SaveChanges());
        }

        //PUT: api/Rooms
        [HttpPut("/UpdateRoomComment/{id}")]
        public IActionResult UpdateRoomComment(int id, UpdateRoom _room)
        {
            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }

            room.roomComment = _room.roomComment;
            return Ok(_context.SaveChanges());
        }

        //PUT: api/Rooms
        [HttpPut("/UpdateRoomPrice/{id}")]
        public IActionResult UpdateRoomPrice(int id, UpdateRoom _room)
        {
            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }

            room.roomPrice = _room.roomPrice;
            return Ok(_context.SaveChanges());
        }

        // DELETE: api/Rooms
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            return Ok(_context.SaveChanges());
        }
    }
}
