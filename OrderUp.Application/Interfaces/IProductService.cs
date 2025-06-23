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
    Task<List<ProductDto>> GetAllProductsAsync(Guid vendorId);
    Task<ProductDto?> GetProductByIdAsync(Guid id);
  }
}