// File: OrderUp.Application/DTOs/VendorDto.cs
using OrderUp.Domain.Enums;

namespace OrderUp.Application.DTOs;

public class VendorDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public Guid VendorTypeId { get; set; }
  public string Location { get; set; } = string.Empty;
  public string? PhoneNumber { get; set; }
  public string Email { get; set; } = null!;
  public Subscription Subscription { get; set; }
  public string? VendorTypeName { get; set; } // Optional for read
}