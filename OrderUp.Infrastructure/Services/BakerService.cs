using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OrderUp.Infrastructure.Services
{
  public class BakerService : IBakerService
  {
    private readonly AppDbContext _context;

    public BakerService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<BakerDto> CreateBakerAsync(BakerDto dto)
    {
      var entity = new Baker { Id = Guid.NewGuid(), Name = dto.Name, Email = dto.Email };
      _context.Bakers.Add(entity);
      await _context.SaveChangesAsync();
      return new BakerDto { Id = entity.Id, Name = entity.Name, Email = entity.Email };
    }

    public async Task DeleteBakerAsync(Guid id)
    {
      var baker = await _context.Bakers.FindAsync(id);
      if (baker != null)
      {
        _context.Bakers.Remove(baker);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<List<BakerDto>> GetAllBakersAsync()
    {
      return await _context.Bakers.Select(b => new BakerDto { Id = b.Id, Name = b.Name, Email = b.Email }).ToListAsync();
    }

    public async Task<BakerDto> GetBakerAsync(Guid id)
    {
      var b = await _context.Bakers.FindAsync(id);
      return b == null ? null : new BakerDto { Id = b.Id, Name = b.Name, Email = b.Email };
    }

    public async Task<BakerDto> UpdateBakerAsync(BakerDto dto)
    {
      var b = await _context.Bakers.FindAsync(dto.Id);
      if (b != null)
      {
        b.Name = dto.Name;
        b.Email = dto.Email;
        await _context.SaveChangesAsync();
      }
      return dto;
    }
  }
}