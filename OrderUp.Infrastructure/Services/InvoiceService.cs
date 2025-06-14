// File: OrderUp.Infrastructure/Services/InvoiceService.cs
using Microsoft.EntityFrameworkCore;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace OrderUp.Infrastructure.Services
{
  public class InvoiceService : IInvoiceService
  {
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;

    public InvoiceService(AppDbContext context, IEmailService emailService)
    {
      _context = context;
      _emailService = emailService;
    }

    public async Task<byte[]> GenerateInvoicePdfAsync(Guid orderId, bool emailToCustomer = false)
    {
      var order = await _context.Orders
          .Include(o => o.OrderItems)
          .ThenInclude(oi => oi.Product)
          .FirstOrDefaultAsync(o => o.Id == orderId);

      if (order == null) throw new Exception("Order not found.");

      var pdf = new PdfDocument();
      var page = pdf.AddPage();
      var gfx = XGraphics.FromPdfPage(page);
      var font = new XFont("Verdana", 12);

      double y = 40;
      gfx.DrawString($"Invoice for Order #{order.Id}", font, XBrushes.Black, new XRect(20, y, page.Width, 20), XStringFormats.TopLeft);
      y += 30;
      gfx.DrawString($"Customer Email: {order.CustomerEmail}", font, XBrushes.Black, new XRect(20, y, page.Width, 20), XStringFormats.TopLeft);
      y += 30;

      foreach (var item in order.OrderItems)
      {
        gfx.DrawString($"{item.Product?.Name} x {item.Quantity}", font, XBrushes.Black, new XRect(20, y, page.Width, 20), XStringFormats.TopLeft);
        y += 25;
      }

      using var stream = new MemoryStream();
      pdf.Save(stream, false);
      var bytes = stream.ToArray();

      if (emailToCustomer)
      {
        //await _emailService.SendEmailAsync(order.CustomerEmail,
        //    $"Invoice for Order #{order.Id}",
        //    "Please find attached your invoice.",
        //    bytes,
        //    $"invoice-{order.Id}.pdf");
      }

      return bytes;
    }
  }
}
