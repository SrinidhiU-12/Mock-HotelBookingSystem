
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.Booking;

public class CreateBookingDto
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Range(1, 20)]
    public int NumberOfGuests { get; set; }
}
