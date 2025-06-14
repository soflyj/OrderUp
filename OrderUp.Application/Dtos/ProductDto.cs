using System;
using System.Collections.Generic;

namespace OrderUp.Application.DTOs
{
  public class ProductDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid BakerId { get; set; }
    public List<byte[]> Images { get; set; } = new();
  }
}