using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Storefront.Settings;

namespace Storefront.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;
        private readonly HttpClient _http;


        public EmailService(IOptions<EmailSettings> settings, IHttpClientFactory httpFactory)
        {
            _settings = settings.Value;
            _http = httpFactory.CreateClient();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var payload = new
            {
                sender = new { name = _settings.FromName, email = _settings.From },
                to = new[] { new { email = toEmail } },
                subject = subject,
                htmlContent = body
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("api-key", _settings.ApiKey);

            var response = await _http.PostAsync("https://api.brevo.com/v3/smtp/email", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Brevo API error: {response.StatusCode} - {error}");
            }

        }
    }
}