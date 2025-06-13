using OrderUp.Domain.Enums;

namespace OrderUp.Application.DTOs.Auth;

public class RegisterRequest
{
    public Guid TenantId { get; set; }             // Tenant user belongs to
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }              // Enum: Vendor, Customer, Admin, etc.
}
