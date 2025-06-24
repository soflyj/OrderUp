namespace OrderUp.Domain.Entities;

public class Product : BaseEntity
{
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public ICollection<ProductInventoryItem> RequiredInventory { get; set; } = new List<ProductInventoryItem>();
  public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}
