
using HotelManagement.API.DTOs.RoomCategory;

namespace HotelManagement.API.Services.Interfaces;

public interface IRoomCategoryService
{
    Task<RoomCategoryResponseDto> CreateAsync(
        CreateRoomCategoryDto request);

    Task<List<RoomCategoryResponseDto>> GetAllAsync();

    Task<RoomCategoryResponseDto> GetByIdAsync(int id);

    Task<RoomCategoryResponseDto> UpdateAsync(
        int id, UpdateRoomCategoryDto request);

    Task DeleteAsync(int id);
}
