using System;

namespace OrderUp.Application.DTOs
{
  public class IngredientDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
  }
}