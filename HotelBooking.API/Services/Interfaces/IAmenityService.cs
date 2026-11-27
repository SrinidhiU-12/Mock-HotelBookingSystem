
using HotelManagement.API.DTOs.Amenity;

namespace HotelManagement.API.Services.Interfaces;

public interface IAmenityService
{
    Task<AmenityResponseDto> CreateAsync(CreateAmenityDto request);

    Task<List<AmenityResponseDto>> GetAllAsync();

    Task<AmenityResponseDto> GetByIdAsync(int id);

    Task<AmenityResponseDto> UpdateAsync(
        int id, UpdateAmenityDto request);

    Task DeleteAsync(int id);

    Task AssignToHotelAsync(AssignAmenityDto request);

    Task<List<AmenityResponseDto>> GetByHotelIdAsync(int hotelId);
}
