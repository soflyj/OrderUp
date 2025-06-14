using System;
using System.Collections.Generic;

namespace OrderUp.Domain.Entities
{
  public class Product : BaseEntity
  {
    public Guid BakerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Baker Baker { get; set; }
    public ICollection<ProductImage> Images { get; set; }
    public Recipe Recipe { get; set; }
  }
}