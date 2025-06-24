// File: OrderUp.Application/Interfaces/IVendorService.cs
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IVendorService
  {
    /// <summary>
    /// Creates a new vendor.
    /// </summary>
    Task<VendorDto> CreateAsync(CreateVendorDto dto);

    /// <summary>
    /// Retrieves all vendors in the system.
    /// </summary>
    Task<List<VendorDto>> GetAllAsync();

    /// <summary>
    /// Retrieves a single vendor by ID.
    /// </summary>
    Task<VendorDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing vendor.
    /// </summary>
    Task<VendorDto> UpdateAsync(VendorDto dto);

    /// <summary>
    /// Deletes a vendor by ID.
    /// </summary>
    Task DeleteAsync(Guid id);
  }
}
