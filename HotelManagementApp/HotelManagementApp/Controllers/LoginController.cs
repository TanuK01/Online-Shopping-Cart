using Microsoft.AspNetCore.Mvc;
using HotelManagementApp.Models;

namespace HotelManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public LoginController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Post(Employee emp)
        {

        }
    }
}
