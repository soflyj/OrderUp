using Microsoft.EntityFrameworkCore;
using OrderUp.Application.Dtos;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;

public class OrderService : IOrderService
{
  private readonly AppDbContext _db;

  public OrderService(AppDbContext db)
  {
    _db = db;
  }

  public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
  {
    var order = new Order
    {
      Id = Guid.NewGuid(),
      VendorId = dto.VendorId,
      CustomerEmail = dto.CustomerEmail,
      NeededByDate = dto.NeededByDate,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };

    _db.Orders.Add(order);

    foreach (var item in dto.OrderItems)
    {
      _db.OrderItems.Add(new OrderItem
      {
        Id = Guid.NewGuid(),
        OrderId = order.Id,
        ProductId = item.ProductId,
        Quantity = item.Quantity
      });
    }

    await _db.SaveChangesAsync();

    return new OrderDto
    {
      Id = order.Id,
      VendorId = order.VendorId,
      CustomerEmail = order.CustomerEmail,
      NeededByDate = order.NeededByDate,
      OrderItems = dto.OrderItems.Select(i => new OrderItemDto
      {
        Id = Guid.NewGuid(), // New ID for the item
        OrderId = order.Id,
        ProductId = i.ProductId,
        Quantity = i.Quantity
      }).ToList()
    };

  }

  /// <summary>
  /// Get all orders for a given vendor.
  /// </summary>
  public async Task<List<OrderDto>> GetOrdersByVendorAsync(Guid vendorId)
  {
    var orders = await _db.Orders
        .Include(o => o.OrderItems)
        .Where(o => o.VendorId == vendorId)
        .ToListAsync();

    return orders.Select(o => new OrderDto
    {
      Id = o.Id,
      VendorId = o.VendorId,
      CustomerEmail = o.CustomerEmail,
      NeededByDate = o.NeededByDate,     
      OrderItems = o.OrderItems.Select(oi => new OrderItemDto
      {
        Id = oi.Id,
        OrderId = oi.OrderId,
        ProductId = oi.ProductId,
        Quantity = oi.Quantity
      }).ToList()
    }).ToList();
  }

  /// <summary>
  /// Get a single order by its ID.
  /// </summary>
  public async Task<OrderDto?> GetOrderAsync(Guid orderId)
  {
    var order = await _db.Orders
        .Include(o => o.OrderItems)
        .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null) return null;

    return new OrderDto
    {
      Id = order.Id,
      VendorId = order.VendorId,
      CustomerEmail = order.CustomerEmail,
      NeededByDate = order.NeededByDate,
      OrderItems = order.OrderItems.Select(oi => new OrderItemDto
      {
        Id = oi.Id,
        OrderId = oi.OrderId,
        ProductId = oi.ProductId,
        Quantity = oi.Quantity
      }).ToList()
    };
  }
}
