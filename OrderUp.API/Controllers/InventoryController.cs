// File: OrderUp.API/Controllers/InventoryController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;

namespace OrderUp.API.Controllers
{
  [ApiController]
  [Route("api/vendors/{vendorId}/inventory")]
  [Authorize]
  public class InventoryController : ControllerBase
  {
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
      _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetInventory(Guid vendorId)
    {
      var items = await _inventoryService.GetInventoryAsync(vendorId);
      return Ok(items);
    }

    [HttpGet("{itemId}")]
    public async Task<IActionResult> GetInventoryItem(Guid vendorId, Guid itemId)
    {
      var item = await _inventoryService.GetInventoryItemAsync(vendorId, itemId);
      return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> AddInventoryItem(Guid vendorId, [FromBody] InventoryItemDto item)
    {
      await _inventoryService.AddInventoryItemAsync(vendorId, item);
      return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateInventoryItem(Guid vendorId, [FromBody] InventoryItemDto item)
    {
      await _inventoryService.UpdateInventoryItemAsync(vendorId, item);
      return Ok();
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> DeleteInventoryItem(Guid vendorId, Guid itemId)
    {
      await _inventoryService.DeleteInventoryItemAsync(vendorId, itemId);
      return Ok();
    }
  }
}
