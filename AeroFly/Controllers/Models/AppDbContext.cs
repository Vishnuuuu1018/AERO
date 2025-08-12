using Microsoft.EntityFrameworkCore;
namespace AeroFly.Controllers.Models

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder) { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KALYAN\\SQLEXPRESS;Database=AirFlyDb;Trusted_Connection=True;TrustServerCertificate=True;");
            //base.OnConfiguring(optionsBuilder);
        }*/
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<int>(); // 🔁 Store Role enum as integer

            modelBuilder.Entity<Flight>().Property(f => f.Fare).HasPrecision(18, 2);
            modelBuilder.Entity<Booking>().Property(b => b.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Payment>().Property(p => p.AmountPaid).HasPrecision(18, 2);
            modelBuilder.Entity<Refund>().Property(r => r.RefundAmount).HasPrecision(18, 2);

            // Booking → User
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Booking → Flight
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FlightId)
                .OnDelete(DeleteBehavior.Restrict);
            

            // Flight → Owner
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Owner)
                .WithMany(u => u.OwnedFlights)
                .HasForeignKey(f => f.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Refund
            modelBuilder.Entity<Refund>()
                .HasOne(r => r.Booking)
                .WithOne(b => b.Refund)
                .HasForeignKey<Refund>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Booking → Seats
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Booking)
                .WithMany(b => b.Seats)
                .HasForeignKey(s => s.BookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Seat → Flight
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Flight)
                .WithMany(f => f.Seats)
                .HasForeignKey(s => s.FlightId)
                .OnDelete(DeleteBehavior.Restrict);
        }







    }
}
