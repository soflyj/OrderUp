using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Domain.Enums;
using OrderUp.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrderUp.Application.DTOs.Auth;

namespace OrderUp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(
        IUserService userService,
        IEmailService emailService,
        IOptions<JwtSettings> jwtOptions)
    {
        _userService = userService;
        _emailService = emailService;
        _jwtSettings = jwtOptions.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        var existingUser = await _userService.GetUserByEmailAsync(model.Email);
        if (existingUser != null)
            return BadRequest("Email already exists.");

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            TenantId = model.TenantId,
            Role = model.Role,
            EmailConfirmationToken = Guid.NewGuid().ToString() // ✅ add this
        };

        await _userService.RegisterUserAsync(user, model.Password);

        // Email verification (simulated)
        await _emailService.SendEmailAsync(model.Email, "Verify your account",
            "Thank you for registering. Click here to verify your email.");

        return Ok("User registered successfully. Verification email sent.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        var user = await _userService.GetUserByEmailAsync(model.Email);
        if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
    {
        var user = await _userService.GetUserByEmailAsync(model.Email);
        if (user == null)
            return BadRequest("No user found with that email.");

        var resetLink = $"https://yourfrontend.com/reset-password?token=dummy-token&email={user.Email}";
        await _emailService.SendEmailAsync(user.Email, "Reset Password", $"Reset your password: {resetLink}");

        return Ok("Reset password link sent.");
    }

    // You can add a real reset-password endpoint later
    // with proper token validation and expiration.

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("userId", user.Id.ToString()),
            new Claim("tenantId", user.TenantId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(15);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPassword(string entered, string storedHash)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(entered);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash) == storedHash;
    }
}
