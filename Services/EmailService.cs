using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Storefront.Settings;

namespace Storefront.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_settings.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            message.To.Add(toEmail);

            await client.SendMailAsync(message);
        }
    }
}