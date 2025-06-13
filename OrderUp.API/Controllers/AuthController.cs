using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderUp.Application.DTOs.Auth;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Domain.Enums;
using OrderUp.Infrastructure.Repositories;
using OrderUp.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderUp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IUserService _userService;
  private readonly IUserRepository _userRepository;
  private readonly IEmailSender _emailSender;
  private readonly JwtSettings _jwtSettings;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IConfiguration _config;

  public AuthController(
      IUserService userService,
      IEmailSender emailSender,
      IPasswordHasher passwordHasher,
      IConfiguration config,
      IOptions<JwtSettings> jwtOptions)
  {
    _userService = userService;
    _emailSender = emailSender;
    _passwordHasher = passwordHasher; // Correctly assign the parameter to the field
    _config = config;
    _jwtSettings = jwtOptions.Value;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequest request)
  {
    var existing = await _userService.GetUserByEmailAsync(request.Email);
    if (existing != null)
      return BadRequest("Email already registered.");

    var token = Guid.NewGuid().ToString();

    var user = new User
    {
      Id = Guid.NewGuid(),
      TenantId = request.TenantId,
      Username = request.Username,
      Email = request.Email,
      PasswordHash = _passwordHasher.Hash(request.Password),
      EmailConfirmationToken = token,
      Role = UserRole.Customer
    };

    await _userService.RegisterUserAsync(user, user.PasswordHash);

    var verificationUrl = $"{_config["AppSettings:FrontendUrl"]}/verify-email?token={token}";

    var message = $"""
        Hi {user.Username},

        Please verify your email by clicking the link below:
        {verificationUrl}

        If you did not register, ignore this email.
        """;

    await _emailSender.SendEmailAsync(user.Email, "Verify your OrderUp account", message);

    return Ok("Registration successful. Check your email to verify your account.");
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
    await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"Reset your password: {resetLink}");

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

  [HttpGet("verify-email")]
  public async Task<IActionResult> VerifyEmail([FromQuery] string token)
  {
    var user = await _userRepository.GetByConfirmationTokenAsync(token);
    if (user == null)
      return NotFound("Invalid or expired token.");

    user.IsEmailConfirmed = true;
    user.EmailConfirmationToken = null;
    user.UpdatedAt = DateTime.UtcNow;

    await _userService.UpdateUserAsync(user);

    return Ok("Email verified successfully.");
  }

}
