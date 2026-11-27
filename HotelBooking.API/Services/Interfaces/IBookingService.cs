
using HotelManagement.API.DTOs.Booking;

namespace HotelManagement.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateAsync(
            int userId,
            CreateBookingDto request);

        Task<List<BookingResponseDto>> GetMyBookingsAsync(
            int userId);

        Task<BookingResponseDto> GetByIdAsync(
            int id,
            int userId);

        Task CancelAsync(
            int id,
            int userId);
    }
}
