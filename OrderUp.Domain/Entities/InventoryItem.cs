namespace OrderUp.Domain.Entities;

public class InventoryItem : BaseEntity
{
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;
  public string Name { get; set; } = null!;
  public int Quantity { get; set; }
  public decimal Cost { get; set; }
}
