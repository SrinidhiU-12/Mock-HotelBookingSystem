
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.Amenity;

public class UpdateAmenityDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
