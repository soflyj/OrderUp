namespace OrderUp.Domain.Entities;

public class Order : BaseEntity
{
  public Guid VendorId { get; set; }
  public Vendor Vendor { get; set; } = null!;
  public string CustomerEmail { get; set; } = null!;
  public DateTime NeededByDate { get; set; }
  public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  public Invoice? Invoice { get; set; }
}
