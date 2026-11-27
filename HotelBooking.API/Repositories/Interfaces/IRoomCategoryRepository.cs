
using MockHotelBookingSystem.Models;

namespace HotelManagement.API.Repositories.Interfaces
{
    public interface IRoomCategoryRepository
    {
        Task AddAsync(RoomCategory category);

        Task<List<RoomCategory>> GetAllAsync();

        Task<RoomCategory?> GetByIdAsync(int id);

        Task<bool> HotelExistsAsync(int hotelId);

        Task<bool> CategoryNameExistsAsync(
            int hotelId,
            string categoryName,
            int? excludeId = null);

        Task UpdateAsync(RoomCategory category);

        Task DeleteAsync(RoomCategory category);
    }
}
