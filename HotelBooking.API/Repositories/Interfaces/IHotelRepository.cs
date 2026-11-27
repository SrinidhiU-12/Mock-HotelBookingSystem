
using MockHotelBookingSystem.Models;

namespace HotelManagement.API.Repositories.Interfaces
{
    public interface IHotelRepository
    {
        Task AddAsync(Hotel hotel);

        Task<List<Hotel>> GetAllAsync();

        Task<Hotel?> GetByIdAsync(int id);

        Task SaveChangesAsync();
    }
}
