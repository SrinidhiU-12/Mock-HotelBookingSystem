
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.Booking;

public class UpdateBookingStatusDto
{
    [Required]
    public string BookingStatus { get; set; } = string.Empty;
}
