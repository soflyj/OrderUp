// File: OrderUp.Application/DTOs/InventoryItemDto.cs
namespace OrderUp.Application.DTOs;

public class InventoryItemDto
{
  public Guid Id { get; set; }
  public Guid VendorId { get; set; }
  public string Name { get; set; } = string.Empty;
  public int Quantity { get; set; }
  public decimal Cost { get; set; }
}