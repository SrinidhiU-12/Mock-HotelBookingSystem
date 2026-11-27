
using HotelManagement.API.DTOs.Hotel;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Services.Interfaces;

namespace HotelManagement.API.Services.Implementations;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<HotelResponseDto> CreateHotelAsync(
        CreateHotelRequestDto request)
    {
        var hotel = new Hotel
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            City = request.City,
            State = request.State,
            Country = request.Country,
            StarRating = request.StarRating,
            IsActive = true
        };

        await _hotelRepository.AddAsync(hotel);
        await _hotelRepository.SaveChangesAsync();

        return MapToResponse(hotel);
    }

    public async Task<List<HotelResponseDto>> GetAllHotelsAsync()
    {
        var hotels = await _hotelRepository.GetAllAsync();

        return hotels
            .Where(h => h.IsActive)
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<HotelResponseDto?> GetHotelByIdAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);

        if (hotel == null || !hotel.IsActive)
        {
            return null;
        }

        return MapToResponse(hotel);
    }

    public async Task<HotelResponseDto?> UpdateHotelAsync(
        int id,
        UpdateHotelRequestDto request)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);

        if (hotel == null || !hotel.IsActive)
        {
            return null;
        }

        hotel.Name = request.Name;
        hotel.Description = request.Description;
        hotel.Address = request.Address;
        hotel.City = request.City;
        hotel.State = request.State;
        hotel.Country = request.Country;
        hotel.StarRating = request.StarRating;

        await _hotelRepository.SaveChangesAsync();

        return MapToResponse(hotel);
    }

    public async Task<bool> DeactivateHotelAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);

        if (hotel == null || !hotel.IsActive)
        {
            return false;
        }

        hotel.IsActive = false;

        await _hotelRepository.SaveChangesAsync();

        return true;
    }

    private static HotelResponseDto MapToResponse(Hotel hotel)
    {
        return new HotelResponseDto
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Description = hotel.Description,
            Address = hotel.Address,
            City = hotel.City,
            State = hotel.State,
            Country = hotel.Country,
            StarRating = hotel.StarRating,
            IsActive = hotel.IsActive
        };
    }
}
