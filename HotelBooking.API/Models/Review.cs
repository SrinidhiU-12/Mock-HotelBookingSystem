namespace MockHotelBookingSystem.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser? User { get; set; }
        public Hotel? Hotel { get; set; }
    }
}