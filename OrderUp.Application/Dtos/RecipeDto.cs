public class RecipeDto
{
  public Guid ProductId { get; set; }
  public List<RecipeItemDto> Ingredients { get; set; } = new();
}