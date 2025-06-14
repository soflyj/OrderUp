using System;

namespace OrderUp.Domain.Entities
{
  public class ProductImage : BaseEntity
  {
    public Guid ProductId { get; set; }
    public byte[] ImageData { get; set; }
    public Product Product { get; set; }
  }
}