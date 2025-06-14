// File: OrderUp.Application/Interfaces/IRecipeService.cs
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IRecipeService
  {
    Task<RecipeDto> GetRecipeAsync(Guid productId);
    Task SetRecipeAsync(RecipeDto recipe);
    Task DeleteRecipeAsync(Guid productId);
  }
}
