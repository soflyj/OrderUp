namespace OrderUp.Application.Interfaces;

public interface IEmailService
{
  Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
  Task SendOrderConfirmationAsync(string toEmail, string subject, string body);
  Task SendInvoiceAsync(string toEmail, string subject, byte[] pdfData);
}
