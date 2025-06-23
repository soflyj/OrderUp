using Microsoft.AspNetCore.Mvc;
using OrderUp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
  private readonly IInvoiceService _invoiceService;

  public InvoicesController(IInvoiceService invoiceService)
  {
    _invoiceService = invoiceService;
  }

  [HttpGet("{orderId}")]
  public async Task<IActionResult> Download(Guid orderId)
  {
    try
    {
      var pdf = await _invoiceService.GenerateInvoicePdfAsync(orderId);
      return File(pdf, "application/pdf", $"invoice-{orderId}.pdf");
    }
    catch (Exception ex)
    {
      return BadRequest(new { error = ex.Message });
    }
  }
}
