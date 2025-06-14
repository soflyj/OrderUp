// File: OrderUp.API/Controllers/OrderController.cs
using Microsoft.AspNetCore.Mvc;
using OrderUp.Application.Interfaces;

namespace OrderUp.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService _orderService;
    private readonly IInvoiceService _invoiceService;

    public OrderController(IOrderService orderService, IInvoiceService invoiceService)
    {
      _orderService = orderService;
      _invoiceService = invoiceService;
    }

    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrder([FromBody] OrderDto order)
    {
      try
      {
        var result = await _orderService.CreateOrderAsync(order);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("invoice/{orderId}")]
    public async Task<IActionResult> DownloadInvoice(Guid orderId)
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
}
