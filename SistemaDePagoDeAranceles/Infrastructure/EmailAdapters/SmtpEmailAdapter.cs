using System.Net;
using System.Net.Mail;
using SistemaDePagoDeAranceles.Domain.Common;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;
namespace SistemaDePagoDeAranceles.Infrastructure.EmailAdapters;



public class SmtpEmailAdapter : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SmtpEmailAdapter> _logger;

    public SmtpEmailAdapter(IConfiguration configuration, ILogger<SmtpEmailAdapter> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
    {
        try
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPass = _configuration["Email:SmtpPassword"];
            var fromEmail = _configuration["Email:FromEmail"];
            var fromName = _configuration["Email:FromName"] ?? "Sistema de Pagos";

            if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(smtpUser))
            {
                _logger.LogWarning("Email configuration is missing. Email will not be sent.");
                return false;
            }

            using var message = new MailMessage();
            message.From = new MailAddress(fromEmail ?? smtpUser, fromName);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            using var smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(message);
            _logger.LogInformation("Email sent successfully to {Email}", toEmail);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
            return false;
        }
    }
}