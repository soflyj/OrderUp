using System;
using System.Collections.Generic;

namespace OrderUp.Domain.Entities
{
  public class Order : BaseEntity
  {
    public Guid BakerId { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime RequiredBy { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public Baker Baker { get; set; }
  }
}