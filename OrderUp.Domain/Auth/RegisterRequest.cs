using OrderUp.Domain.Enums;

namespace OrderUp.Application.DTOs.Auth;

public class RegisterRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public required UserRole Role { get; set; }              // Enum: Vendor, Customer, Admin, etc.
}
