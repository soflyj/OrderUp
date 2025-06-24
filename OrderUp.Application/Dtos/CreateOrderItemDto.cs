// File: OrderUp.Application/DTOs/CreateOrderItemDto.cs
namespace OrderUp.Application.DTOs
{
  public class CreateOrderItemDto
  {
    /// <summary>
    /// The ID of the product being ordered.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of this product to include in the order.
    /// </summary>
    public int Quantity { get; set; }
  }
}
