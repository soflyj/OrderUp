using System;
using System.Collections.Generic;

namespace OrderUp.Domain.Entities
{
  public class Recipe : BaseEntity
  {
    public Guid ProductId { get; set; }
    public ICollection<RecipeItem> RecipeItems { get; set; }
  }
}