// File: OrderUp.Infrastructure/Services/OrderService.cs
using Microsoft.EntityFrameworkCore;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;
using System.Net.Mail;
using System.Text;

namespace OrderUp.Infrastructure.Services
{
  public class OrderService : IOrderService
  {
    private readonly AppDbContext _db;
    private readonly IEmailService _email;

    public OrderService(AppDbContext db, IEmailService email)
    {
      _db = db;
      _email = email;
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto dto)
    {
      var entity = new Order
      {
        Id = Guid.NewGuid(),
        BakerId = dto.BakerId,
        CustomerEmail = dto.CustomerEmail,
        NeededByDate = dto.NeededByDate,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Items = dto.Items.Select(i => new OrderItem
        {
          Id = Guid.NewGuid(),
          ProductId = i.ProductId,
          Quantity = i.Quantity
        }).ToList()
      };

      _db.Orders.Add(entity);
      await _db.SaveChangesAsync();

      // Generate invoice PDF (optional)
      var invoicePdf = await _invoiceService.GenerateInvoicePdfAsync(entity.Id);

      // Email customer
      await _emailService.SendEmailAsync(
          dto.CustomerEmail,
          "Order Confirmation",
          $"Thank you for your order. Needed by: {dto.NeededByDate:d}",
          invoicePdf,
          $"Order_{entity.Id}.pdf"
      );

      // Email baker
      var baker = await _db.Bakers.FindAsync(dto.BakerId);
      if (baker != null)
      {
        await _emailService.SendEmailAsync(
            baker.Email,
            "New Order Received",
            $"An order was placed and is due by: {dto.NeededByDate:d}",
            invoicePdf,
            $"Order_{entity.Id}.pdf"
        );
      }

      return dto;
    }

    public async Task<List<OrderDto>> GetOrdersByBakerAsync(Guid bakerId)
    {
      return await _db.Orders
          .Include(o => o.OrderItems)
          .Where(o => o.BakerId == bakerId)
          .Select(o => new OrderDto
          {
            Id = o.Id,
            BakerId = o.BakerId,
            CustomerEmail = o.CustomerEmail,
            RequiredDate = o.RequiredDate,
            Items = o.OrderItems.Select(i => new OrderItemDto
            {
              ProductId = i.ProductId,
              Quantity = i.Quantity
            }).ToList()
          }).ToListAsync();
    }

    public async Task<OrderDto> GetOrderAsync(Guid orderId)
    {
      var order = await _db.Orders
          .Include(o => o.OrderItems)
          .FirstOrDefaultAsync(o => o.Id == orderId);

      if (order == null) return null;

      return new OrderDto
      {
        Id = order.Id,
        BakerId = order.BakerId,
        CustomerEmail = order.CustomerEmail,
        RequiredDate = order.RequiredDate,
        Items = order.OrderItems.Select(i => new OrderItemDto
        {
          ProductId = i.ProductId,
          Quantity = i.Quantity
        }).ToList()
      };
    }
  }
}
