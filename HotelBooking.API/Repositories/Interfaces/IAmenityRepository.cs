
using MockHotelBookingSystem.Models;

namespace HotelManagement.API.Repositories.Interfaces
{
    public interface IAmenityRepository
    {
        Task AddAsync(Amenity amenity);

        Task<List<Amenity>> GetAllAsync();

        Task<Amenity?> GetByIdAsync(int id);

        Task<bool> NameExistsAsync(
            string name,
            int? excludeId = null);

        Task UpdateAsync(Amenity amenity);

        Task DeleteAsync(Amenity amenity);

        Task<bool> HotelExistsAsync(int hotelId);

        Task<List<Amenity>> GetByIdsAsync(List<int> ids);

        Task AssignToHotelAsync(int hotelId, List<int> amenityIds);

        Task<List<Amenity>> GetByHotelIdAsync(int hotelId);
    }
}
