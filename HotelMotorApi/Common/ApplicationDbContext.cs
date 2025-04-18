using HotelMotorShared.Models;
using Microsoft.EntityFrameworkCore;
using HotelMotorShared.DTOs;

namespace HotelMotorApi.Common
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<Service> Services { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<HotelMotorShared.DTOs.CustomerDTO> CustomerDTO { get; set; } = default!;
    }
}
