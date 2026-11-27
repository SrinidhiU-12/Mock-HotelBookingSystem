namespace MockHotelBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; } = "Pending";
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser? User { get; set; }
        public Room? Room { get; set; }
    }
}