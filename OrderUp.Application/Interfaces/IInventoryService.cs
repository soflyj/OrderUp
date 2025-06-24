// File: OrderUp.Application/Interfaces/IInventoryService.cs
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IInventoryService
  {
    Task<List<InventoryItemDto>> GetInventoryAsync(Guid vendorId);
    Task<InventoryItemDto?> GetInventoryItemAsync(Guid vendorId, Guid itemId);
    Task AddInventoryItemAsync(Guid vendorId, InventoryItemDto item);
    Task UpdateInventoryItemAsync(Guid vendorId, InventoryItemDto item);
    Task DeleteInventoryItemAsync(Guid vendorId, Guid itemId);
  }
}
