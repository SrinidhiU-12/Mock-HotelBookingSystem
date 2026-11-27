
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.Room;

public class CreateRoomDto
{
    [Required]
    public int RoomCategoryId { get; set; }

    [Required]
    [StringLength(20)]
    public string RoomNumber { get; set; } = string.Empty;

    [Range(0, 100)]
    public int FloorNumber { get; set; }

    [Required]
    public string RoomStatus { get; set; } = "Available";
}
