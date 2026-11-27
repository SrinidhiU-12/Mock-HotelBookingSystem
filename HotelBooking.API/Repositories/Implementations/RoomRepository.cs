
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;

namespace HotelManagement.API.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> RoomCategoryExistsAsync(int categoryId)
        {
            return await _context.RoomCategories
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<bool> RoomNumberExistsAsync(
            int categoryId,
            string roomNumber,
            int? excludeId = null)
        {
            return await _context.Rooms.AnyAsync(r =>
                r.RoomCategoryId == categoryId &&
                r.RoomNumber == roomNumber &&
                (!excludeId.HasValue || r.Id != excludeId.Value));
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}
