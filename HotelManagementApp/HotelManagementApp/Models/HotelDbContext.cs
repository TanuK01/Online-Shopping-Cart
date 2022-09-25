using HotelManagementApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelManagementApp.Models
{
    public class HotelDbContext: DbContext
    {
        public HotelDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }

        
    }
}
