
using HotelManagement.API.DTOs.Room;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Services.Interfaces;

namespace HotelManagement.API.Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoomResponseDto> CreateAsync(
        CreateRoomDto request)
    {
        if (!await _repository.RoomCategoryExistsAsync(
            request.RoomCategoryId))
        {
            throw new KeyNotFoundException(
                "Room category not found.");
        }

        if (await _repository.RoomNumberExistsAsync(
            request.RoomCategoryId, request.RoomNumber))
        {
            throw new InvalidOperationException(
                "Room number already exists.");
        }

        var room = new Room
        {
            RoomCategoryId = request.RoomCategoryId,
            RoomNumber = request.RoomNumber,
            FloorNumber = request.FloorNumber,
            RoomStatus = request.RoomStatus
        };

        await _repository.AddAsync(room);

        return MapToResponse(room);
    }

    public async Task<List<RoomResponseDto>> GetAllAsync()
    {
        var rooms = await _repository.GetAllAsync();

        return rooms.Select(MapToResponse).ToList();
    }

    public async Task<RoomResponseDto> GetByIdAsync(int id)
    {
        var room = await _repository.GetByIdAsync(id);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        return MapToResponse(room);
    }

    public async Task<RoomResponseDto> UpdateAsync(
        int id, UpdateRoomDto request)
    {
        var room = await _repository.GetByIdAsync(id);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        if (await _repository.RoomNumberExistsAsync(
            room.RoomCategoryId, request.RoomNumber, id))
        {
            throw new InvalidOperationException(
                "Room number already exists.");
        }

        room.RoomNumber = request.RoomNumber;
        room.FloorNumber = request.FloorNumber;
        room.RoomStatus = request.RoomStatus;

        await _repository.UpdateAsync(room);

        return MapToResponse(room);
    }

    public async Task DeleteAsync(int id)
    {
        var room = await _repository.GetByIdAsync(id);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        await _repository.DeleteAsync(room);
    }

    private static RoomResponseDto MapToResponse(Room room)
    {
        return new RoomResponseDto
        {
            Id = room.Id,
            RoomCategoryId = room.RoomCategoryId,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            RoomStatus = room.RoomStatus
        };
    }
}
