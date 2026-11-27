
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;

namespace HotelManagement.API.Repositories.Implementations
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Hotel?> GetByIdAsync(int id)
        {
            return await _context.Hotels
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
