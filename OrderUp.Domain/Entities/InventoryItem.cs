namespace OrderUp.Domain.Entities;

public class InventoryItem
{
  public Guid Id { get; set; }
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;

  public string Name { get; set; } = null!;
  public int Quantity { get; set; }
  public decimal Cost { get; set; }
}
