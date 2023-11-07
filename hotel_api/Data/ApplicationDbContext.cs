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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
         .Property(b => b.TotalPrice)
         .HasColumnType("decimal(18, 2)"); // DECIMAL với 18 chữ số tổng cộng và 2 chữ số sau dấu thập phân

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Stars)
                .HasColumnType("decimal(3, 1)"); // DECIMAL với 3 chữ số tổng cộng và 1 chữ số sau dấu thập phân

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)"); // DECIMAL với 18 chữ số tổng cộng và 2 chữ số sau dấu thập phân

            modelBuilder.Entity<Staff>()
                .Property(s => s.Salary)
                .HasColumnType("decimal(18, 2)");
        }

    }
}