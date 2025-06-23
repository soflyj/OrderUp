using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;

//InventoryController(for managing stock)

//  OrdersController(vendor or staff)

//InvoicesController(vendor or admin)
[Authorize] //[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly IProductService _productService;

  public ProductsController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpGet("{vendorId}")]
  public async Task<IActionResult> GetAll(Guid vendorId)
      => Ok(await _productService.GetAllProductsAsync(vendorId));

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] ProductDto dto)
  {
    var result = await _productService.CreateProductAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
  }

  [HttpGet("by-id/{id}")]
  public async Task<IActionResult> GetById(Guid id)
  {
    var p = await _productService.GetProductByIdAsync(id);
    return p == null ? NotFound() : Ok(p);
  }

  [HttpPut]
  public async Task<IActionResult> Update([FromBody] ProductDto dto)
  {
    var updated = await _productService.UpdateProductAsync(dto);
    return Ok(updated);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await _productService.DeleteProductAsync(id);
    return NoContent();
  }
}
