
using MockHotelBookingSystem.Models;

namespace HotelManagement.API.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room);

        Task<List<Room>> GetAllAsync();

        Task<Room?> GetByIdAsync(int id);

        Task<bool> RoomCategoryExistsAsync(int categoryId);

        Task<bool> RoomNumberExistsAsync(
            int categoryId,
            string roomNumber,
            int? excludeId = null);

        Task UpdateAsync(Room room);

        Task DeleteAsync(Room room);
    }
}
