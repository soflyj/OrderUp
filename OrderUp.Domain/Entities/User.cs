namespace OrderUp.Domain.Entities;

public enum UserRole
{
  Admin,
  Manager,
  Staff
}

public class User
{
  public Guid Id { get; set; }
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;

  public string Username { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string PasswordHash { get; set; } = null!;
  public bool IsEmailConfirmed { get; set; }
  public string? EmailConfirmationToken { get; set; }

  public UserRole Role { get; set; }

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
