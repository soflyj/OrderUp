namespace OrderUp.Application.Dtos
{
  public class InvoiceDto
  {
    public Guid OrderId { get; set; }
    public bool EmailToCustomer { get; set; }
  }
}