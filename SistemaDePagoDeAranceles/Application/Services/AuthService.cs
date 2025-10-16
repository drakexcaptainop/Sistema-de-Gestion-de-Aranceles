using System.Security.Cryptography;
using System.Text;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;

namespace SistemaDePagoDeAranceles.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbRepository<User> _userRepo;
        private readonly IUserRepository _userQuery;

        public AuthService(IDbRepository<User> userRepo, IUserRepository userQuery)
        {
            _userRepo = userRepo;
            _userQuery = userQuery;
        }

        public (bool ok, string? role, string? error) ValidateLogin(string username, string plainPassword)
        {
            var user = _userQuery.GetByUsername(username);
            if (user is null || !user.Status) return (false, null, "Usuario no encontrado o inactivo.");

            var givenHash = Md5Hex(plainPassword);
            if (!string.Equals(user.PasswordHash, givenHash, System.StringComparison.OrdinalIgnoreCase))
                return (false, null, "Contraseña incorrecta.");

            return (true, user.Role, null);
        }

        public (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterUser(string firstName, string lastName, string email, string role, int createdBy)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
                return (false, null, null, "Faltan datos obligatorios.");

            // Generate username: first.last (lower) + optional numeric suffix if exists
            var baseUser = (firstName + "." + lastName).ToLower().Replace(" ", "");
            var candidate = baseUser;
            int suffix = 1;
            while (_userQuery.GetByUsername(candidate) != null)
            {
                candidate = baseUser + suffix.ToString();
                suffix++;
            }

            // Generate random password (10 chars)
            var pwd = GeneratePassword(10);

            var user = new User
            {
                Username = candidate,
                PasswordHash = Md5Hex(pwd),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Role = role,
                CreatedBy = createdBy,
                CreatedDate = System.DateTime.UtcNow,
                LastUpdate = System.DateTime.UtcNow,
                Status = true
            };

            var inserted = _userRepo.Insert(user);
            if (inserted <= 0) return (false, null, null, "No se pudo insertar el usuario.");
            return (true, candidate, pwd, null);
        }

        private static string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%";
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[bytes[i] % chars.Length]);
            }
            return sb.ToString();
        }

        public static string Md5Hex(string input)
        {
            using var md5 = MD5.Create();
            var data = Encoding.UTF8.GetBytes(input);
            var hash = md5.ComputeHash(data);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
