using System;

namespace OrderUp.Domain.Entities
{
  public class Ingredient : BaseEntity
  {
    public string Name { get; set; }
    public string Unit { get; set; } // e.g. grams, ml, etc.
  }
}