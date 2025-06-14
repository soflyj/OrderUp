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
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<List<IngredientDto>> GetIngredientsAsync(Guid bakerId)
    {
      return await _context.Ingredients
          .Where(i => i.BakerId == bakerId)
          .Select(i => new IngredientDto
          {
            Id = i.Id,
            Name = i.Name,
            Unit = i.Unit
          }).ToListAsync();
    }

    public async Task AddIngredientAsync(Guid bakerId, IngredientDto dto)
    {
      var entity = new Ingredient
      {
        Id = Guid.NewGuid(),
        BakerId = bakerId,
        Name = dto.Name,
        Unit = dto.Unit
      };

      _context.Ingredients.Add(entity);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateIngredientAsync(Guid bakerId, IngredientDto dto)
    {
      var entity = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == dto.Id && i.BakerId == bakerId);
      if (entity != null)
      {
        entity.Name = dto.Name;
        entity.Unit = dto.Unit;
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteIngredientAsync(Guid bakerId, Guid ingredientId)
    {
      var entity = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientId && i.BakerId == bakerId);
      if (entity != null)
      {
        _context.Ingredients.Remove(entity);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<List<InventoryItemDto>> GetInventoryAsync(Guid bakerId)
    {
      return await _context.Inventories
          .Include(inv => inv.Ingredient)
          .Where(inv => inv.BakerId == bakerId)
          .Select(inv => new InventoryItemDto
          {
            IngredientId = inv.IngredientId,
            Quantity = inv.Quantity
          }).ToListAsync();
    }

    public async Task UpdateInventoryItemAsync(Guid bakerId, InventoryItemDto item)
    {
      var inventory = await _context.Inventories
          .FirstOrDefaultAsync(i => i.BakerId == bakerId && i.IngredientId == item.IngredientId);

      if (inventory != null)
      {
        inventory.Quantity = item.Quantity;
      }
      else
      {
        inventory = new Inventory
        {
          Id = Guid.NewGuid(),
          BakerId = bakerId,
          IngredientId = item.IngredientId,
          Quantity = item.Quantity
        };
        _context.Inventories.Add(inventory);
      }

      await _context.SaveChangesAsync();
    }
  }
}
