
using HotelManagement.API.DTOs.Hotel;
using HotelManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    // Get all active hotels
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await _hotelService.GetAllHotelsAsync();

        return Ok(hotels);
    }

    // Get hotel by ID
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await _hotelService.GetHotelByIdAsync(id);

        if (hotel == null)
        {
            return NotFound(new
            {
                message = "Hotel not found."
            });
        }

        return Ok(hotel);
    }

    // Create hotel
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateHotel(
        CreateHotelRequestDto request)
    {
        var hotel = await _hotelService.CreateHotelAsync(request);

        return CreatedAtAction(
            nameof(GetHotelById),
            new { id = hotel.Id },
            hotel
        );
    }

    // Update hotel
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateHotel(
        int id,
        UpdateHotelRequestDto request)
    {
        var hotel = await _hotelService.UpdateHotelAsync(id, request);

        if (hotel == null)
        {
            return NotFound(new
            {
                message = "Hotel not found."
            });
        }

        return Ok(hotel);
    }

    // Deactivate hotel
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeactivateHotel(int id)
    {
        var result = await _hotelService.DeactivateHotelAsync(id);

        if (!result)
        {
            return NotFound(new
            {
                message = "Hotel not found."
            });
        }

        return Ok(new
        {
            message = "Hotel deactivated successfully."
        });
    }
}
