// File: OrderUp.Application/DTOs/VendorDto.cs
namespace OrderUp.Application.DTOs;

public class VendorDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public Guid VendorTypeId { get; set; }
  public string? VendorTypeName { get; set; } // Optional for read
}