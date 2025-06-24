namespace OrderUp.Domain.Entities;

public class Invoice : BaseEntity
{
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = null!;
  public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
}
