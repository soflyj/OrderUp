namespace OrderUp.Domain.Entities;

public class ProductInventoryItem
{
  public Guid Id { get; set; }

  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;

  public string IngredientName { get; set; } = null!;
  public int QuantityRequired { get; set; }
}
