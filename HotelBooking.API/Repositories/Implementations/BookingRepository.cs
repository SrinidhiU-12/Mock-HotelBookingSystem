
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;

namespace HotelManagement.API.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Room?> GetRoomWithCategoryAsync(int roomId)
        {
            return await _context.Rooms
                .Include(r => r.RoomCategory)
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<bool> HasOverlappingBookingAsync(
            int roomId,
            DateTime checkIn,
            DateTime checkOut)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                b.BookingStatus != "Cancelled" &&
                checkIn < b.CheckOutDate &&
                checkOut > b.CheckInDate);
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
