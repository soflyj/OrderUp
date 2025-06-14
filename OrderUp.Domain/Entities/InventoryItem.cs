using System;

namespace OrderUp.Domain.Entities
{
  public class InventoryItem : BaseEntity
  {
    public Guid BakerId { get; set; }
    public Guid IngredientId { get; set; }
    public double Quantity { get; set; }
    public Baker Baker { get; set; }
    public Ingredient Ingredient { get; set; }
  }
}