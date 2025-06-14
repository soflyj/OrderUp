using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IProductService
  {
    Task<ProductDto> CreateProductAsync(ProductDto dto);
    Task<ProductDto> UpdateProductAsync(ProductDto dto);
    Task DeleteProductAsync(Guid id);
  }
}