
namespace MockHotelBookingSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomCategoryId { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int FloorNumber { get; set; }

        public string RoomStatus { get; set; } = "Available";

        public int MaximumGuests { get; set; } = 2;

        public RoomCategory? RoomCategory { get; set; }
    }
}
