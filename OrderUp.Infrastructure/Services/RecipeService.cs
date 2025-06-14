// File: OrderUp.Infrastructure/Services/RecipeService.cs
using Microsoft.EntityFrameworkCore;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;

namespace OrderUp.Infrastructure.Services
{
  public class RecipeService : IRecipeService
  {
    private readonly AppDbContext _db;

    public RecipeService(AppDbContext db)
    {
      _db = db;
    }

    public async Task<RecipeDto> GetRecipeAsync(Guid productId)
    {
      var items = await _db.Recipes
          .Where(r => r.ProductId == productId)
          .ToListAsync();

      return new RecipeDto
      {
        ProductId = productId,
        Ingredients = items.Select(r => new RecipeItemDto
        {
          IngredientId = r.IngredientId,
          Quantity = r.Quantity
        }).ToList()
      };
    }

    public async Task SetRecipeAsync(RecipeDto recipe)
    {
      var existing = await _db.Recipes
          .Where(r => r.ProductId == recipe.ProductId)
          .ToListAsync();

      _db.Recipes.RemoveRange(existing);

      var newItems = recipe.Ingredients.Select(i => new Recipe
      {
        Id = Guid.NewGuid(),
        ProductId = recipe.ProductId,
        IngredientId = i.IngredientId,
        Quantity = i.Quantity
      });

      _db.Recipes.AddRange(newItems);
      await _db.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(Guid productId)
    {
      var existing = await _db.Recipes
          .Where(r => r.ProductId == productId)
          .ToListAsync();

      _db.Recipes.RemoveRange(existing);
      await _db.SaveChangesAsync();
    }
  }
}
