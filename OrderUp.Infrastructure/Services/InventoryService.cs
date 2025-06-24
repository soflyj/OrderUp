// File: OrderUp.Infrastructure/Services/InventoryService.cs
using Microsoft.EntityFrameworkCore;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;

namespace OrderUp.Infrastructure.Services
{
  public class InventoryService : IInventoryService
  {
    private readonly AppDbContext _db;

    public InventoryService(AppDbContext db)
    {
      _db = db;
    }

    public async Task<List<InventoryItemDto>> GetInventoryAsync(Guid vendorId)
    {
      return await _db.InventoryItems
          .Where(i => i.VendorId == vendorId)
          .Select(i => new InventoryItemDto
          {
            Id = i.Id,
            VendorId = i.VendorId,
            Name = i.Name,
            Quantity = i.Quantity,
            Cost = i.Cost
          })
          .ToListAsync();
    }

    public async Task<InventoryItemDto?> GetInventoryItemAsync(Guid vendorId, Guid itemId)
    {
      var item = await _db.InventoryItems
          .FirstOrDefaultAsync(i => i.Id == itemId && i.VendorId == vendorId);

      return item == null ? null : new InventoryItemDto
      {
        Id = item.Id,
        VendorId = item.VendorId,
        Name = item.Name,
        Quantity = item.Quantity,
        Cost = item.Cost
      };
    }

    public async Task AddInventoryItemAsync(Guid vendorId, InventoryItemDto item)
    {
      var entity = new InventoryItem
      {
        Id = Guid.NewGuid(),
        VendorId = vendorId,
        Name = item.Name,
        Quantity = item.Quantity,
        Cost = item.Cost
      };

      _db.InventoryItems.Add(entity);
      await _db.SaveChangesAsync();
    }

    public async Task UpdateInventoryItemAsync(Guid vendorId, InventoryItemDto item)
    {
      var entity = await _db.InventoryItems
          .FirstOrDefaultAsync(i => i.Id == item.Id && i.VendorId == vendorId);

      if (entity != null)
      {
        entity.Name = item.Name;
        entity.Quantity = item.Quantity;
        entity.Cost = item.Cost;
        await _db.SaveChangesAsync();
      }
    }

    public async Task DeleteInventoryItemAsync(Guid vendorId, Guid itemId)
    {
      var item = await _db.InventoryItems
          .FirstOrDefaultAsync(i => i.Id == itemId && i.VendorId == vendorId);

      if (item != null)
      {
        _db.InventoryItems.Remove(item);
        await _db.SaveChangesAsync();
      }
    }
  }
}
