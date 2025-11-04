namespace SistemaDePagoDeAranceles.Domain.Ports.ServicePorts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
    }
}
