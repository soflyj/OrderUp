namespace OrderUp.Application.DTOs.Order;

public class CreateOrderRequestDto
{
  public Guid BakerId { get; set; }
  public string CustomerEmail { get; set; }
  public string CustomerName { get; set; }
  public DateTime RequiredDate { get; set; }
  public List<OrderItemDto> Items { get; set; }
}
