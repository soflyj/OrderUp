using OrderUp.Domain.Enums;

namespace OrderUp.Domain.Entities;

public class Vendor: BaseEntity
{
  public string Name { get; set; } = null!;
  public Guid VendorTypeId { get; set; }
  public VendorType VendorType { get; set; } = null!;
  public string Location { get; set; } = null!;
  public string? PhoneNumber { get; set; }
  public string Email { get; set; } = null!;
  public Subscription Subscription { get; set; }
  public ICollection<User> Users { get; set; } = new List<User>();
  public ICollection<Product> Products { get; set; } = new List<Product>();
  public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
  public ICollection<Order> Orders { get; set; } = new List<Order>();
}
