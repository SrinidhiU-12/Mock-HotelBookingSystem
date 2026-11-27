
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;

namespace HotelManagement.API.Repositories.Implementations
{
    public class RoomCategoryRepository : IRoomCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RoomCategory category)
        {
            await _context.RoomCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomCategory>> GetAllAsync()
        {
            return await _context.RoomCategories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RoomCategory?> GetByIdAsync(int id)
        {
            return await _context.RoomCategories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> HotelExistsAsync(int hotelId)
        {
            return await _context.Hotels
                .AnyAsync(h => h.Id == hotelId);
        }

        public async Task<bool> CategoryNameExistsAsync(
            int hotelId,
            string categoryName,
            int? excludeId = null)
        {
            return await _context.RoomCategories.AnyAsync(c =>
                c.HotelId == hotelId &&
                c.CategoryName == categoryName &&
                (!excludeId.HasValue || c.Id != excludeId.Value));
        }

        public async Task UpdateAsync(RoomCategory category)
        {
            _context.RoomCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoomCategory category)
        {
            _context.RoomCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
