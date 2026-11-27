
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using HotelManagement.API.DTOs.Auth;
using HotelManagement.API.Services.Interfaces;

using MockHotelBookingSystem.Data;
using MockHotelBookingSystem.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<ApplicationUser> _passwordHasher;

    public AuthService(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<ApplicationUser>();
    }

    // CUSTOMER REGISTRATION
    public async Task<AuthResponseDto> RegisterAsync(
        RegisterRequestDto request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        // Check whether email already exists
        var existingUser = await _context.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "An account with this email already exists.");
        }

        // Create new customer
        var user = new ApplicationUser
        {
            FullName = request.FullName.Trim(),
            Email = email,
            PhoneNumber = request.PhoneNumber.Trim(),
            Role = "Customer"
        };

        // Hash password
        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.Password
        );

        _context.ApplicationUsers.Add(user);

        await _context.SaveChangesAsync();

        // Generate JWT
        return GenerateAuthResponse(user);
    }

    // CUSTOMER LOGIN
    public async Task<AuthResponseDto> LoginAsync(
        LoginRequestDto request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _context.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email);

        if (user == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // Verify password
        var verificationResult =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

        if (verificationResult ==
            PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // Update password hash if the hashing algorithm needs upgrading
        if (verificationResult ==
            PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                request.Password
            );

            await _context.SaveChangesAsync();
        }

        return GenerateAuthResponse(user);
    }

    // GENERATE JWT TOKEN
    private AuthResponseDto GenerateAuthResponse(
        ApplicationUser user)
    {
        var jwtKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        if (string.IsNullOrWhiteSpace(jwtKey) ||
            Encoding.UTF8.GetByteCount(jwtKey) < 32)
        {
            throw new InvalidOperationException(
                "JWT key must contain at least 32 bytes.");
        }

        if (string.IsNullOrWhiteSpace(issuer) ||
            string.IsNullOrWhiteSpace(audience))
        {
            throw new InvalidOperationException(
                "JWT issuer and audience must be configured.");
        }

        var expiryMinutes = int.TryParse(
            _configuration["Jwt:ExpiryMinutes"],
            out var configuredExpiry)
                ? configuredExpiry
                : 60;

        if (expiryMinutes <= 0)
        {
            throw new InvalidOperationException(
                "JWT expiry must be greater than zero.");
        }

        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var header = new
        {
            alg = "HS256",
            typ = "JWT"
        };

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var expiry = new DateTimeOffset(expiresAt)
            .ToUnixTimeSeconds();

        var payload = new Dictionary<string, object>
        {
            ["iss"] = issuer,
            ["aud"] = audience,
            ["sub"] = user.Id.ToString(),
            [ClaimTypes.NameIdentifier] = user.Id.ToString(),
            [ClaimTypes.Name] = user.FullName,
            [ClaimTypes.Email] = user.Email,
            [ClaimTypes.Role] = user.Role,
            ["iat"] = now,
            ["exp"] = expiry
        };

        var headerJson = JsonSerializer.Serialize(header);
        var payloadJson = JsonSerializer.Serialize(payload);

        var encodedHeader = Base64UrlEncode(
            Encoding.UTF8.GetBytes(headerJson));

        var encodedPayload = Base64UrlEncode(
            Encoding.UTF8.GetBytes(payloadJson));

        var unsignedToken = $"{encodedHeader}.{encodedPayload}";

        var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

        var signatureBytes = HMACSHA256.HashData(
            keyBytes,
            Encoding.UTF8.GetBytes(unsignedToken));

        var signature = Base64UrlEncode(signatureBytes);

        var token = $"{unsignedToken}.{signature}";

        return new AuthResponseDto
        {
            UserId = user.Id.ToString(),
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            Token = token,
            ExpiresAt = expiresAt
        };
    }

    // BASE64 URL ENCODING
    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
