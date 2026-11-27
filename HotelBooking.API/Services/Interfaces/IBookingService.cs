
using HotelManagement.API.DTOs.Booking;

namespace HotelManagement.API.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateAsync(
        string userId,
        CreateBookingDto request);

    Task<List<BookingResponseDto>> GetMyBookingsAsync(
        string userId);

    Task<BookingResponseDto> GetByIdAsync(
        int id,
        string userId);

    Task CancelAsync(int id, string userId);
}
