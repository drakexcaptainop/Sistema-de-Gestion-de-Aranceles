namespace SistemaDePagoDeAranceles.Domain.Ports.ServicePorts
{
    public interface IAuthService
    {
        (bool ok, string? role, string? error) ValidateLogin(string username, string plainPassword);
        (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterUser(string firstName, string lastName, string email, string role, int createdBy);
    }
}
