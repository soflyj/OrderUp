using System;
using System.Collections.Generic;

namespace OrderUp.Domain.Entities
{
  public class Baker : BaseEntity
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<InventoryItem> Inventory { get; set; }
  }
}