using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagementApp.Models;

namespace HotelManagementApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HotelDbContext _context;

        public EmployeesController(HotelDbContext context)
        {
            this._context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_context.Employees.ToList());
        }

        // GET: api/Employees
        [HttpGet("{id}")]
        public IActionResult SearchEmployees(int id)
        {
            var emp = _context.Employees.Find(id);

            if (emp == null)
            {
                return NotFound();
            }

            return Ok(emp);
        }

        // POST: api/Employees
        [HttpPost]
        public void AddEmployee(Employee addEmployee)
        {
            _context.Employees.Add(addEmployee);
            _context.SaveChanges();
        }

        // PUT: api/Employees
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updateEmployee)
        {
            var emp = _context.Employees.Find(id);

            if (emp == null)
            {
                return NotFound();
            }

            emp.employeeName = updateEmployee.employeeName;

            return Ok(_context.SaveChanges());

        }

        // DELETE: api/Employees
        [HttpDelete("{id}")]
        public IActionResult RemoveEmployee(int id)
        {
            var emp = _context.Employees.Find(id);

            if (emp != null)
            {
                _context.Remove(emp);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }
}
