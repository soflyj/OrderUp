using Microsoft.EntityFrameworkCore;
using OrderUp.Application.Interfaces;
using OrderUp.Infrastructure.Persistence;
using QuestPDF.Fluent;

public class InvoiceService : IInvoiceService
{
  private readonly AppDbContext _db;

  public InvoiceService(AppDbContext db)
  {
    _db = db;
  }

  public async Task<byte[]> GenerateInvoicePdfAsync(Guid orderId)
  {
    var order = await _db.Orders
        .Include(o => o.OrderItems).ThenInclude(i => i.Product)
        .Include(o => o.Vendor)
        .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null)
      throw new Exception("Order not found");

    var doc = Document.Create(container =>
    {
      container.Page(page =>
      {
        page.Margin(30);
        page.Header().Text($"Invoice - {order.Id}").Bold().FontSize(18);
        page.Content().Table(table =>
        {
          table.ColumnsDefinition(c => { c.RelativeColumn(); c.ConstantColumn(100); });
          table.Header(h =>
          {
            h.Cell().Text("Product");
            h.Cell().Text("Quantity");
          });

          foreach (var item in order.OrderItems)
          {
            table.Cell().Text(item.Product.Name);
            table.Cell().Text(item.Quantity.ToString());
          }
        });
        page.Footer().AlignRight().Text($"Issued: {DateTime.UtcNow:yyyy-MM-dd}");
      });
    });

    return doc.GeneratePdf();
  }
}
