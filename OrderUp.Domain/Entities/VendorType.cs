// File: OrderUp.Domain/Entities/VendorType.cs
namespace OrderUp.Domain.Entities;

public class VendorType : BaseEntity
{
  public string Name { get; set; } = null!; // E.g. Baker, PetGroomer, Gardener
  public ICollection<GeneralInventoryItem> GeneralInventoryItems { get; set; } = new List<GeneralInventoryItem>();
  public ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
