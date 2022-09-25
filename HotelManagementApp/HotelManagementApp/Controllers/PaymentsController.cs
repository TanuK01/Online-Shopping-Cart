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
    public class PaymentsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public PaymentsController(HotelDbContext context)
        {
            _context = context;
        }



        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(Payment addPayment)
        {

            _context.Payments.Add(addPayment);
            _context.SaveChanges();

            return Ok(_context.Payments.ToList());
        }


    }
}
