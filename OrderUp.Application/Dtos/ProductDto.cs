// File: OrderUp.Application/DTOs/ProductDto.cs
namespace OrderUp.Application.DTOs;

public class ProductDto
{
  public Guid Id { get; set; }
  public Guid VendorId { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public List<string> ImageUrls { get; set; } = new();
  public List<ProductIngredientDto> RequiredIngredients { get; set; } = new();
}

public class ProductIngredientDto
{
  public string IngredientName { get; set; } = string.Empty;
  public int QuantityRequired { get; set; }
}