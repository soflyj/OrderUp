namespace OrderUp.Domain.Entities;

public class Order
{
  public Guid Id { get; set; }
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;

  public string CustomerEmail { get; set; } = null!;
  public DateTime NeededByDate { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  public Invoice? Invoice { get; set; }
}
