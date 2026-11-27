
using HotelManagement.API.DTOs.RoomCategory;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Services.Interfaces;

namespace HotelManagement.API.Services.Implementations;

public class RoomCategoryService : IRoomCategoryService
{
    private readonly IRoomCategoryRepository _repository;

    public RoomCategoryService(
        IRoomCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoomCategoryResponseDto> CreateAsync(
        CreateRoomCategoryDto request)
    {
        if (!await _repository.HotelExistsAsync(request.HotelId))
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        if (await _repository.CategoryNameExistsAsync(
            request.HotelId, request.CategoryName))
        {
            throw new InvalidOperationException(
                "Category name already exists for this hotel.");
        }

        var category = new RoomCategory
        {
            HotelId = request.HotelId,
            CategoryName = request.CategoryName,
            Description = request.Description,
            PricePerNight = request.PricePerNight,
            MaximumGuests = request.MaximumGuests
        };

        await _repository.AddAsync(category);

        return MapToResponse(category);
    }

    public async Task<List<RoomCategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories.Select(MapToResponse).ToList();
    }

    public async Task<RoomCategoryResponseDto> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            throw new KeyNotFoundException(
                "Room category not found.");
        }

        return MapToResponse(category);
    }

    public async Task<RoomCategoryResponseDto> UpdateAsync(
        int id, UpdateRoomCategoryDto request)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            throw new KeyNotFoundException(
                "Room category not found.");
        }

        if (await _repository.CategoryNameExistsAsync(
            category.HotelId, request.CategoryName, id))
        {
            throw new InvalidOperationException(
                "Category name already exists for this hotel.");
        }

        category.CategoryName = request.CategoryName;
        category.Description = request.Description;
        category.PricePerNight = request.PricePerNight;
        category.MaximumGuests = request.MaximumGuests;

        await _repository.UpdateAsync(category);

        return MapToResponse(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            throw new KeyNotFoundException(
                "Room category not found.");
        }

        await _repository.DeleteAsync(category);
    }

    private static RoomCategoryResponseDto MapToResponse(
        RoomCategory category)
    {
        return new RoomCategoryResponseDto
        {
            Id = category.Id,
            HotelId = category.HotelId,
            CategoryName = category.CategoryName,
            Description = category.Description,
            PricePerNight = category.PricePerNight,
            MaximumGuests = category.MaximumGuests
        };
    }
}
