// File: OrderUp.Application/Interfaces/IInvoiceService.cs
using System.Threading.Tasks;

namespace OrderUp.Application.Interfaces
{
  public interface IInvoiceService
  {
    Task<byte[]> GenerateInvoicePdfAsync(Guid orderId);
  }
}
