
using HotelManagement.API.DTOs.Hotel;

namespace HotelManagement.API.Services.Interfaces;

public interface IHotelService
{
    Task<HotelResponseDto> CreateHotelAsync(
        CreateHotelRequestDto request);

    Task<List<HotelResponseDto>> GetAllHotelsAsync();

    Task<HotelResponseDto?> GetHotelByIdAsync(int id);

    Task<HotelResponseDto?> UpdateHotelAsync(
        int id,
        UpdateHotelRequestDto request);

    Task<bool> DeactivateHotelAsync(int id);
}
