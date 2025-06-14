// File: OrderUp.Application/Interfaces/IOrderService.cs
using OrderUp.Application.DTOs;

namespace OrderUp.Application.Interfaces
{
  public interface IOrderService
  {
    Task<OrderDto> CreateOrderAsync(OrderDto order);
    Task<List<OrderDto>> GetOrdersByBakerAsync(Guid bakerId);
    Task<OrderDto> GetOrderAsync(Guid orderId);
  }
}
