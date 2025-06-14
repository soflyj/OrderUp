using System;

namespace OrderUp.Domain.Entities
{
  public class Invoice : BaseEntity
  {
    public Guid OrderId { get; set; }
    public byte[] PdfData { get; set; }
    public bool IsSent { get; set; }
    public Order Order { get; set; }
  }
}