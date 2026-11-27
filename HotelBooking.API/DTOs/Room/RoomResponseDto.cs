
namespace HotelManagement.API.DTOs.Room;

public class RoomResponseDto
{
    public int Id { get; set; }

    public int RoomCategoryId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int FloorNumber { get; set; }

    public string RoomStatus { get; set; } = string.Empty;
}
