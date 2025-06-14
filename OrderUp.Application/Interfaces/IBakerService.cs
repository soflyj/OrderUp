using System;
using System.Threading.Tasks;
using OrderUp.Application.DTOs;
using System.Collections.Generic;

namespace OrderUp.Application.Interfaces
{
  public interface IBakerService
  {
    Task<BakerDto> GetBakerAsync(Guid id);
    Task<List<BakerDto>> GetAllBakersAsync();
    Task<BakerDto> CreateBakerAsync(BakerDto baker);
    Task<BakerDto> UpdateBakerAsync(BakerDto baker);
    Task DeleteBakerAsync(Guid id);
  }
}