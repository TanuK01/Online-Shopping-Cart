using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementApp.Models;

namespace HotelManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public BillsController(HotelDbContext context)
        {
           this._context = context;
        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult IssueBill(AddBill _createBill)
        {
            var createBill = new Bill();
            {
                createBill.billAmount = _createBill.billAmount;
                createBill.billDate = _createBill.billDate;
            };

            _context.Bills.Add(createBill);
            return Ok(_context.Bills.ToList());
        }



    }
}
