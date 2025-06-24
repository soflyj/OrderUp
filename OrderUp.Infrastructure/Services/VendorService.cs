using Microsoft.EntityFrameworkCore;
using OrderUp.Application.Dtos;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;

namespace OrderUp.Infrastructure.Services
{
  public class VendorService : IVendorService
  {
    private readonly AppDbContext _db;

    public VendorService(AppDbContext db)
    {
      _db = db;
    }

    public async Task<VendorDto> CreateAsync(CreateVendorDto dto)
    {
      var vendor = new Vendor
      {
        Id = Guid.NewGuid(),
        Name = dto.Name,
        VendorTypeId = dto.VendorTypeId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };

      _db.Vendors.Add(vendor);
      await _db.SaveChangesAsync();

      return new VendorDto
      {
        Id = vendor.Id,
        Name = vendor.Name,
        VendorTypeId = vendor.VendorTypeId
      };
    }

    public async Task<List<VendorDto>> GetAllAsync()
    {
      return await _db.Vendors
          .Select(v => new VendorDto
          {
            Id = v.Id,
            Name = v.Name,
            VendorTypeId = v.VendorTypeId
          })
          .ToListAsync();
    }

    public async Task<VendorDto?> GetByIdAsync(Guid id)
    {
      var vendor = await _db.Vendors.FindAsync(id);
      if (vendor == null) return null;

      return new VendorDto
      {
        Id = vendor.Id,
        Name = vendor.Name,
        VendorTypeId = vendor.VendorTypeId
      };
    }

    public async Task<VendorDto> UpdateAsync(VendorDto dto)
    {
      var vendor = await _db.Vendors.FindAsync(dto.Id);
      if (vendor == null)
        throw new Exception("Vendor not found");

      vendor.Name = dto.Name;
      vendor.VendorTypeId = dto.VendorTypeId;
      vendor.UpdatedAt = DateTime.UtcNow;

      await _db.SaveChangesAsync();

      return dto;
    }

    public async Task DeleteAsync(Guid id)
    {
      var vendor = await _db.Vendors.FindAsync(id);
      if (vendor != null)
      {
        _db.Vendors.Remove(vendor);
        await _db.SaveChangesAsync();
      }
    }
  }
}
