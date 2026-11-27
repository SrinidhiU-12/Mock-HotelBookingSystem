
using HotelManagement.API.DTOs.Room;

namespace HotelManagement.API.Services.Interfaces;

public interface IRoomService
{
    Task<RoomResponseDto> CreateAsync(CreateRoomDto request);

    Task<List<RoomResponseDto>> GetAllAsync();

    Task<RoomResponseDto> GetByIdAsync(int id);

    Task<RoomResponseDto> UpdateAsync(
        int id,
        UpdateRoomDto request);

    Task DeleteAsync(int id);
}
