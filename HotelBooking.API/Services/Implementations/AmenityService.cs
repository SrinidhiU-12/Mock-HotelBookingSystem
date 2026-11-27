
using HotelManagement.API.DTOs.Amenity;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Services.Interfaces;

namespace HotelManagement.API.Services.Implementations;

public class AmenityService : IAmenityService
{
    private readonly IAmenityRepository _repository;

    public AmenityService(IAmenityRepository repository)
    {
        _repository = repository;
    }

    public async Task<AmenityResponseDto> CreateAsync(
        CreateAmenityDto request)
    {
        if (await _repository.NameExistsAsync(request.Name))
        {
            throw new InvalidOperationException(
                "Amenity name already exists.");
        }

        var amenity = new Amenity
        {
            Name = request.Name,
            Description = request.Description
        };

        await _repository.AddAsync(amenity);

        return MapToResponse(amenity);
    }

    public async Task<List<AmenityResponseDto>> GetAllAsync()
    {
        var amenities = await _repository.GetAllAsync();

        return amenities.Select(MapToResponse).ToList();
    }

    public async Task<AmenityResponseDto> GetByIdAsync(int id)
    {
        var amenity = await _repository.GetByIdAsync(id);

        if (amenity == null)
        {
            throw new KeyNotFoundException("Amenity not found.");
        }

        return MapToResponse(amenity);
    }

    public async Task<AmenityResponseDto> UpdateAsync(
        int id, UpdateAmenityDto request)
    {
        var amenity = await _repository.GetByIdAsync(id);

        if (amenity == null)
        {
            throw new KeyNotFoundException("Amenity not found.");
        }

        if (await _repository.NameExistsAsync(request.Name, id))
        {
            throw new InvalidOperationException(
                "Amenity name already exists.");
        }

        amenity.Name = request.Name;
        amenity.Description = request.Description;

        await _repository.UpdateAsync(amenity);

        return MapToResponse(amenity);
    }

    public async Task DeleteAsync(int id)
    {
        var amenity = await _repository.GetByIdAsync(id);

        if (amenity == null)
        {
            throw new KeyNotFoundException("Amenity not found.");
        }

        await _repository.DeleteAsync(amenity);
    }

    public async Task AssignToHotelAsync(AssignAmenityDto request)
    {
        if (!await _repository.HotelExistsAsync(request.HotelId))
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        var ids = request.AmenityIds.Distinct().ToList();

        var amenities = await _repository.GetByIdsAsync(ids);

        if (amenities.Count != ids.Count)
        {
            throw new KeyNotFoundException(
                "One or more amenities were not found.");
        }

        await _repository.AssignToHotelAsync(
            request.HotelId, ids);
    }

    public async Task<List<AmenityResponseDto>> GetByHotelIdAsync(
        int hotelId)
    {
        if (!await _repository.HotelExistsAsync(hotelId))
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        var amenities = await _repository.GetByHotelIdAsync(hotelId);

        return amenities.Select(MapToResponse).ToList();
    }

    private static AmenityResponseDto MapToResponse(Amenity amenity)
    {
        return new AmenityResponseDto
        {
            Id = amenity.Id,
            Name = amenity.Name,
            Description = amenity.Description
        };
    }
}
