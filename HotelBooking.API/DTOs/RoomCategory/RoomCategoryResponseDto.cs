
namespace HotelManagement.API.DTOs.RoomCategory;

public class RoomCategoryResponseDto
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal PricePerNight { get; set; }

    public int MaximumGuests { get; set; }
}
