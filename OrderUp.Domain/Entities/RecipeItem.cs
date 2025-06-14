using System;

namespace OrderUp.Domain.Entities
{
  public class RecipeItem : BaseEntity
  {
    public Guid RecipeId { get; set; }
    public Guid IngredientId { get; set; }
    public double QuantityRequired { get; set; }
    public Ingredient Ingredient { get; set; }
    public Recipe Recipe { get; set; }
  }
}