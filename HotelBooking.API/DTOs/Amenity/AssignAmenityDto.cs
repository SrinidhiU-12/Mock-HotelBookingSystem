
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.Amenity;

public class AssignAmenityDto
{
    [Required]
    public int HotelId { get; set; }

    [Required]
    [MinLength(1)]
    public List<int> AmenityIds { get; set; } = [];
}
