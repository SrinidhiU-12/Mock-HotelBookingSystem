
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs.Hotel;

public class CreateHotelRequestDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    [Range(1, 5)]
    public int StarRating { get; set; }
}
