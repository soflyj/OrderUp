using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OrderUp.Application.Interfaces;
using OrderUp.Infrastructure.Settings;
using System.Net;
using System.Net.Mail;

namespace OrderUp.Infrastructure.Services;

public class EmailService : IEmailService
{
  private readonly SmtpSettings _smtpSettings;
  private readonly IConfiguration _config;

  public EmailService(IOptions<SmtpSettings> smtpSettings, IConfiguration config)
  {
    _smtpSettings = smtpSettings.Value;
    _config = config;
  }

  public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
  {
    using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
    {
      Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
      EnableSsl = _smtpSettings.EnableSsl
    };

    var mailMessage = new MailMessage
    {
      From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
      Subject = subject,
      Body = htmlMessage,
      IsBodyHtml = true,
    };

    mailMessage.To.Add(toEmail);

    await client.SendMailAsync(mailMessage);
  }

  public async Task SendOrderConfirmationAsync(string toEmail, string subject, string body)
  {
    await SendEmailAsync(toEmail, subject, body, null);
  }

  public async Task SendInvoiceAsync(string toEmail, string subject, byte[] pdfData)
  {
    var attachment = new Attachment(new MemoryStream(pdfData), "invoice.pdf");
    await SendEmailAsync(toEmail, subject, "Attached is your invoice.", attachment);
  }

  private async Task SendEmailAsync(string to, string subject, string body, Attachment? attachment)
  {
    var client = new SmtpClient(_config["Smtp:Host"])
    {
      Port = int.Parse(_config["Smtp:Port"]),
      Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
      EnableSsl = true
    };

    var mail = new MailMessage(_config["Smtp:From"], to, subject, body);
    if (attachment != null)
      mail.Attachments.Add(attachment);

    await client.SendMailAsync(mail);
  }
}
