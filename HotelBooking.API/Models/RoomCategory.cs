namespace MockHotelBookingSystem.Models
{
    public class RoomCategory
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }

        public Hotel? Hotel { get; set; }
    }
}