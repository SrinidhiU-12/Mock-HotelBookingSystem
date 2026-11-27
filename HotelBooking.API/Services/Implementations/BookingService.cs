
using HotelManagement.API.DTOs.Booking;
using MockHotelBookingSystem.Models;
using HotelManagement.API.Repositories.Interfaces;
using HotelManagement.API.Services.Interfaces;

namespace HotelManagement.API.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookingResponseDto> CreateAsync(
        int userId,
        CreateBookingDto request)
    {
        if (request.CheckInDate.Date < DateTime.UtcNow.Date)
        {
            throw new InvalidOperationException(
                "Check-in date cannot be in the past.");
        }

        if (request.CheckOutDate.Date <= request.CheckInDate.Date)
        {
            throw new InvalidOperationException(
                "Check-out date must be after check-in date.");
        }

        var room = await _repository.GetRoomWithCategoryAsync(
            request.RoomId);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        if (room.RoomCategory == null)
        {
            throw new InvalidOperationException(
                "Room category not found.");
        }

        if (room.RoomStatus != "Available")
        {
            throw new InvalidOperationException(
                "This room is not available for booking.");
        }

        if (request.NumberOfGuests >
            room.RoomCategory.MaximumGuests)
        {
            throw new InvalidOperationException(
                "Number of guests exceeds room capacity.");
        }

        var hasOverlap =
            await _repository.HasOverlappingBookingAsync(
                request.RoomId,
                request.CheckInDate.Date,
                request.CheckOutDate.Date);

        if (hasOverlap)
        {
            throw new InvalidOperationException(
                "This room is already booked for the selected dates.");
        }

        var numberOfNights =
            (request.CheckOutDate.Date -
             request.CheckInDate.Date).Days;

        var totalAmount =
            room.RoomCategory.PricePerNight * numberOfNights;

        var booking = new Booking
        {
            UserId = userId,
            RoomId = request.RoomId,
            CheckInDate = request.CheckInDate.Date,
            CheckOutDate = request.CheckOutDate.Date,
            NumberOfGuests = request.NumberOfGuests,
            TotalAmount = totalAmount,
            BookingStatus = "Confirmed",
            BookingDate = DateTime.UtcNow
        };

        await _repository.AddAsync(booking);

        return MapToResponse(booking);
    }

    public async Task<List<BookingResponseDto>> GetMyBookingsAsync(
        int userId)
    {
        var bookings = await _repository.GetByUserIdAsync(userId);

        return bookings.Select(MapToResponse).ToList();
    }

    public async Task<BookingResponseDto> GetByIdAsync(
        int id,
        int userId)
    {
        var booking = await _repository.GetByIdAsync(id);

        if (booking == null || booking.UserId != userId)
        {
            throw new KeyNotFoundException("Booking not found.");
        }

        return MapToResponse(booking);
    }

    public async Task CancelAsync(int id, int userId)
    {
        var booking = await _repository.GetByIdAsync(id);

        if (booking == null || booking.UserId != userId)
        {
            throw new KeyNotFoundException("Booking not found.");
        }

        if (booking.BookingStatus == "Cancelled")
        {
            throw new InvalidOperationException(
                "Booking is already cancelled.");
        }

        if (booking.CheckInDate.Date <= DateTime.UtcNow.Date)
        {
            throw new InvalidOperationException(
                "Booking cannot be cancelled on or after check-in.");
        }

        booking.BookingStatus = "Cancelled";

        await _repository.UpdateAsync(booking);
    }

    private static BookingResponseDto MapToResponse(
        Booking booking)
    {
        return new BookingResponseDto
        {
            Id = booking.Id,
            UserId = booking.UserId.ToString(),
            RoomId = booking.RoomId,
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,
            NumberOfGuests = booking.NumberOfGuests,
            TotalAmount = booking.TotalAmount,
            BookingStatus = booking.BookingStatus,
            BookingDate = booking.BookingDate
        };
    }
}
