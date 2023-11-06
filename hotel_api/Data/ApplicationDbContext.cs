using Hotels.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<Staff> Staff { get; set; }
    }
}