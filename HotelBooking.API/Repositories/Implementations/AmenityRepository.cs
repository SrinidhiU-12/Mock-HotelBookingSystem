
using Microsoft.EntityFrameworkCore;
using MockHotelBookingSystem.Data;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;

namespace HotelManagement.API.Repositories.Implementations
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public AmenityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Amenity amenity)
        {
            await _context.Set<Amenity>().AddAsync(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Amenity>> GetAllAsync()
        {
            return await _context.Set<Amenity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Amenity?> GetByIdAsync(int id)
        {
            return await _context.Set<Amenity>()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> NameExistsAsync(
            string name,
            int? excludeId = null)
        {
            return await _context.Set<Amenity>().AnyAsync(a =>
                a.Name == name &&
                (!excludeId.HasValue || a.Id != excludeId.Value));
        }

        public async Task UpdateAsync(Amenity amenity)
        {
            _context.Set<Amenity>().Update(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Amenity amenity)
        {
            _context.Set<Amenity>().Remove(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HotelExistsAsync(int hotelId)
        {
            return await _context.Hotels
                .AnyAsync(h => h.Id == hotelId);
        }

        public async Task<List<Amenity>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Set<Amenity>()
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task AssignToHotelAsync(
            int hotelId,
            List<int> amenityIds)
        {
            var existingIds = await _context.Set<HotelAmenity>()
                .Where(x => x.HotelId == hotelId &&
                            amenityIds.Contains(x.AmenityId))
                .Select(x => x.AmenityId)
                .ToListAsync();

            var newIds = amenityIds.Except(existingIds);

            var assignments = newIds.Select(id => new HotelAmenity
            {
                HotelId = hotelId,
                AmenityId = id
            });

            await _context.Set<HotelAmenity>()
                .AddRangeAsync(assignments);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Amenity>> GetByHotelIdAsync(int hotelId)
        {
            return await (
                from assignment in _context.Set<HotelAmenity>()
                join amenity in _context.Set<Amenity>()
                    on assignment.AmenityId equals amenity.Id
                where assignment.HotelId == hotelId
                select amenity
            ).AsNoTracking().ToListAsync();
        }
    }
}
