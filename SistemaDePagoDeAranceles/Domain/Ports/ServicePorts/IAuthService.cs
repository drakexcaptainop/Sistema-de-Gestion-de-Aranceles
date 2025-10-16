namespace SistemaDePagoDeAranceles.Domain.Ports.ServicePorts
{
    public interface IAuthService
    {
        // Returns ok, userId (if ok), role, error
        (bool ok, int? userId, string? role, string? error) ValidateLogin(string username, string plainPassword);
        (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterUser(string firstName, string lastName, string email, string role, int createdBy);
    }
}
