using Microsoft.EntityFrameworkCore;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;

public class ProductService : IProductService
{
  private readonly AppDbContext _context;

  public ProductService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<ProductDto> CreateProductAsync(ProductDto dto)
  {
    var product = new Product
    {
      Id = Guid.NewGuid(),
      BakerId = dto.BakerId,
      Name = dto.Name,
      Description = dto.Description,
      Price = dto.Price,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };

    _context.Products.Add(product);
    await _context.SaveChangesAsync();
    return new ProductDto { Id = product.Id, BakerId = product.BakerId, Name = product.Name, Description = product.Description, Price = product.Price };
  }

  public async Task DeleteProductAsync(Guid id)
  {
    var p = await _context.Products.FindAsync(id);
    if (p != null)
    {
      _context.Products.Remove(p);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<List<ProductDto>> GetAllProductsAsync()
  {
    return await _context.Products.Select(p => new ProductDto
    {
      Id = p.Id,
      BakerId = p.BakerId,
      Name = p.Name,
      Description = p.Description,
      Price = p.Price
    }).ToListAsync();
  }

  public async Task<ProductDto> GetProductAsync(Guid id)
  {
    var p = await _context.Products.FindAsync(id);
    if (p == null) return null;
    return new ProductDto { Id = p.Id, BakerId = p.BakerId, Name = p.Name, Description = p.Description, Price = p.Price };
  }

  public async Task<ProductDto> UpdateProductAsync(ProductDto dto)
  {
    var p = await _context.Products.FindAsync(dto.Id);
    if (p != null)
    {
      p.Name = dto.Name;
      p.Description = dto.Description;
      p.Price = dto.Price;
      p.UpdatedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
    }
    return dto;
  }
}