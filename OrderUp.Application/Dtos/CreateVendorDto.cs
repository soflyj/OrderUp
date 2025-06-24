// File: OrderUp.Application/DTOs/CreateVendorDto.cs
namespace OrderUp.Application.DTOs
{
  public class CreateVendorDto
  {
    /// <summary>
    /// Name of the vendor (e.g. "Super Bakes", "Happy Pets").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The VendorTypeId that defines what kind of business this is (e.g. Baker, PetGroomer).
    /// </summary>
    public Guid VendorTypeId { get; set; }
  }
}
