// File: OrderUp.Application/Interfaces/IOrderService.cs
using OrderUp.Application.Dtos;
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IOrderService
  {
    Task<OrderDto> CreateOrderAsync(CreateOrderDto order);
    Task<List<OrderDto>> GetOrdersByVendorAsync(Guid VendorId);
    Task<OrderDto> GetOrderAsync(Guid orderId);
  }
}
