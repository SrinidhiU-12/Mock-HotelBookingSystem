
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.RoomCategory;

public class CreateRoomCategoryDto
{
    [Required]
    public int HotelId { get; set; }

    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, 1000000)]
    public decimal PricePerNight { get; set; }

    [Range(1, 20)]
    public int MaximumGuests { get; set; }
}
