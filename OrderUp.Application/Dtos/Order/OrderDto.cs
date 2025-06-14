
public class OrderDto
{
  public Guid Id { get; set; }
  public Guid BakerId { get; set; }
  public string CustomerEmail { get; set; }
  public DateTime RequiredDate { get; set; }
  public List<OrderItemDto> Items { get; set; } = new();
}