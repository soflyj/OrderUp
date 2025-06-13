using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderUp.Application.DTOs.Auth;
using OrderUp.Application.Interfaces;
using OrderUp.Application.Models;
using OrderUp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OrderUp.Application.Services;

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IConfiguration _configuration;
  //private readonly JwtSettings wtSettings;
  private readonly IEmailSender _emailSender;

  public AuthService(
      IUserRepository userRepository,
      IPasswordHasher passwordHasher,
      IConfiguration configuration,
      //IJwtTokenGenerator jwtTokenGenerator,
      IEmailSender emailSender)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _configuration = configuration;
    //_jwtTokenGenerator = jwtTokenGenerator;
    _emailSender = emailSender;
  }

  public async Task<string?> LoginAsync(string email, string password)
  {
    var user = await _userRepository.GetByEmailAsync(email);
    if (user == null) return null;

    // Verify password hash - use BCrypt recommended, example below:
    if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
      return null;

    return GenerateJwtToken(user);
  }

  public async Task RegisterAsync(User user, string password)
  {
    // Hash password using BCrypt
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    user.EmailConfirmationToken = Guid.NewGuid().ToString();

    await _userRepository.AddUserAsync(user);

    // TODO: Send confirmation email with token
  }

  private string GenerateJwtToken(User user)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("TenantId", user.TenantId.ToString())
        };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(15),
      SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public AuthService(IPasswordHasher passwordHasher /* other deps */)
  {
    _passwordHasher = passwordHasher;
  }

  public async Task RegisterAsync(RegisterRequest request)
  {
    var hashed = _passwordHasher.Hash(request.Password);
    // save user
  }
}
