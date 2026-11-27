namespace MockHotelBookingSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomCategoryId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public RoomCategory? RoomCategory { get; set; }
    }
}