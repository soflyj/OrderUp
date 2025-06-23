// File: OrderUp.Application/DTOs/CreateOrderDto.cs
namespace OrderUp.Application.DTOs
{
  public class CreateOrderDto
  {
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the vendor placing the order.
    /// </summary>
    public Guid VendorId { get; set; }

    /// <summary>
    /// Email address of the customer placing the order.
    /// </summary>
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// Date when the customer needs the order ready.
    /// </summary>
    public DateTime NeededByDate { get; set; }

    /// <summary>
    /// List of items in the order (ProductId and Quantity).
    /// </summary>
    public List<CreateOrderItemDto> OrderItems { get; set; } = new();
  }
}
