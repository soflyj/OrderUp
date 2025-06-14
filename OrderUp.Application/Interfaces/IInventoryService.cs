// File: OrderUp.Application/Interfaces/IInventoryService.cs
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IInventoryService
  {
    Task<List<IngredientDto>> GetIngredientsAsync(Guid bakerId);
    Task AddIngredientAsync(Guid bakerId, IngredientDto ingredient);
    Task UpdateIngredientAsync(Guid bakerId, IngredientDto ingredient);
    Task DeleteIngredientAsync(Guid bakerId, Guid ingredientId);

    Task<List<InventoryItemDto>> GetInventoryAsync(Guid bakerId);
    Task UpdateInventoryItemAsync(Guid bakerId, InventoryItemDto item);
  }
}
