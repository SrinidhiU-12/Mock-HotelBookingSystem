
namespace MockHotelBookingSystem.Models
{
    public class Amenity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<HotelAmenity> HotelAmenities { get; set; }
            = new List<HotelAmenity>();
    }
}
