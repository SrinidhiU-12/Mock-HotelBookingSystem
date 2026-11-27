
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Models;

namespace MockHotelBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<RoomCategory> RoomCategories { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<HotelAmenity> HotelAmenities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking precision
            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalAmount)
                .HasPrecision(18, 2);

            // Payment precision
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // Room category precision
            modelBuilder.Entity<RoomCategory>()
                .Property(r => r.PricePerNight)
                .HasPrecision(18, 2);

            // Hotel-Amenity composite key
            modelBuilder.Entity<HotelAmenity>()
                .HasKey(ha => new { ha.HotelId, ha.AmenityId });

            // Hotel relationship
            modelBuilder.Entity<HotelAmenity>()
                .HasOne(ha => ha.Hotel)
                .WithMany()
                .HasForeignKey(ha => ha.HotelId);

            // Amenity relationship
            modelBuilder.Entity<HotelAmenity>()
                .HasOne(ha => ha.Amenity)
                .WithMany(a => a.HotelAmenities)
                .HasForeignKey(ha => ha.AmenityId);
        }
    }
}
