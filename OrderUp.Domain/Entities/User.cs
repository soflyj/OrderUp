namespace OrderUp.Domain.Entities;

public enum UserRole
{
  Admin,
  Manager,
  Staff
}

public class User : BaseEntity
{
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string PasswordHash { get; set; } = null!;
  public bool IsEmailConfirmed { get; set; }
  public string? EmailConfirmationToken { get; set; }
  public UserRole Role { get; set; }
}
