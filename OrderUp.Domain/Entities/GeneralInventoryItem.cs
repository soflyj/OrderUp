namespace OrderUp.Domain.Entities;

public class GeneralInventoryItem
{
  public Guid Id { get; set; }
  public Guid VendorTypeId { get; set; }
  public VendorType VendorType { get; set; } = null!;

  public string Name { get; set; } = null!;
}
