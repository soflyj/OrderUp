// File: OrderUp.Application/DTOs/OrderDto.cs
namespace OrderUp.Application.Dtos;

public class OrderDto
{
  public Guid Id { get; set; }
  public Guid VendorId { get; set; }
  public string CustomerEmail { get; set; } = string.Empty;
  public DateTime NeededByDate { get; set; }
  public List<OrderItemDto> OrderItems { get; set; } = new();
}

public class OrderItemDto
{
  public Guid Id { get; set; }

  public Guid OrderId { get; set; }
  public Guid ProductId { get; set; }
  public int Quantity { get; set; }
}