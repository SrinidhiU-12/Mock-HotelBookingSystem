
namespace HotelManagement.API.DTOs.Booking;

public class BookingResponseDto
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int NumberOfGuests { get; set; }

    public decimal TotalAmount { get; set; }

    public string BookingStatus { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }
}
