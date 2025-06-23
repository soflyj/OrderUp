// File: OrderUp.Application/DTOs/CreateInventoryItemDto.cs
namespace OrderUp.Application.DTOs
{
  public class CreateInventoryItemDto
  {
    /// <summary>
    /// The ID of the vendor this inventory item belongs to.
    /// </summary>
    public Guid VendorId { get; set; }

    /// <summary>
    /// Name of the inventory item (e.g., "Flour", "Shampoo").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of the item available in stock.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Cost per unit of the inventory item.
    /// </summary>
    public decimal Cost { get; set; }
  }
}
