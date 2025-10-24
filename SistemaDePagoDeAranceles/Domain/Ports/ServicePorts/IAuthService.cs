using SistemaDePagoDeAranceles.Domain.Models;

namespace SistemaDePagoDeAranceles.Domain.Ports.ServicePorts
{
    public interface IAuthService
    {
        // Returns ok, userId (if ok), role, error, isFirstLogin
        (bool ok, int? userId, string? role, string? error, bool isFirstLogin) ValidateLogin(string username, string plainPassword);
        
        // Get user by ID
        User? GetUserById(int userId);
        
        // Change password for first login
        Task<(bool ok, string? error)> ChangePasswordFirstLogin(int userId, string newPassword);
        (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterUser(string firstName, string lastName, string email, string role, int createdBy);
    }
}
