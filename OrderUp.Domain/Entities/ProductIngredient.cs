namespace OrderUp.Domain.Entities;

public class ProductIngredient
{
  public Guid Id { get; set; }
  public Guid ProductId { get; set; }
  public Guid IngredientId { get; set; }
  public double Quantity { get; set; } // quantity needed for the product

  public Product Product { get; set; }
  public Ingredient Ingredient { get; set; }
}
