
using MockHotelBookingSystem.Models;

namespace HotelManagement.API.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);

        Task<List<Booking>> GetByUserIdAsync(int userId);

        Task<Booking?> GetByIdAsync(int id);

        Task<Room?> GetRoomWithCategoryAsync(int roomId);

        Task<bool> HasOverlappingBookingAsync(
            int roomId,
            DateTime checkIn,
            DateTime checkOut);

        Task UpdateAsync(Booking booking);
    }
}
