
using HotelManagement.API.DTOs.Auth;

namespace HotelManagement.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
}
