using System.Security.Cryptography;
using System.Text;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

namespace SistemaDePagoDeAranceles.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositoryService _userService;
        private readonly EmailService _emailService;

        // Role mapping between application role names and DB integer codes
        private static readonly Dictionary<string, int> RoleToCode = new()
        {
            { "Admin", 1 },
            { "Contador", 2 }
        };

        private static readonly Dictionary<int, string> CodeToRole = RoleToCode.ToDictionary(kv => kv.Value, kv => kv.Key);

        public AuthService(IUserRepositoryService userService, EmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public (bool ok, int? userId, string? role, string? error, bool isFirstLogin) ValidateLogin(string username, string plainPassword)
        {
            var user = _userService.GetByUsername(username);
            if (user is null || !user.Status) return (false, null, null, "Usuario no encontrado o inactivo.", false);

            var givenHash = Md5Hex(plainPassword);
            if (!string.Equals(user.PasswordHash, givenHash, System.StringComparison.OrdinalIgnoreCase))
                return (false, null, null, "Contraseña incorrecta.", false);

            // DB may store role as numeric code; convert it to application role name if necessary
            var roleValue = user.Role;
            if (int.TryParse(roleValue, out var code) && CodeToRole.ContainsKey(code))
            {
                roleValue = CodeToRole[code];
            }
            return (true, user.Id, roleValue, null, user.FirstLogin == 0);
        }

        public (bool ok, string? generatedUsername, string? generatedPassword, string? error) RegisterUser(string firstName, string lastName, string email, string role, int createdBy)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
                return (false, null, null, "Faltan datos obligatorios.");
            
            var baseUser = email.Split('@')[0].ToLower().Replace(" ", "");
            var candidate = baseUser;
            int suffix = 1;
            while (_userService.GetByUsername(candidate) != null)
            {
                candidate = baseUser + suffix.ToString();
                suffix++;
            }

            // Generate random password (10 chars)
            var pwd = GeneratePassword(10);

            // Map application role name to DB code (DB expects integer role); store as string representation of code
            var roleCode = RoleToCode.ContainsKey(role) ? RoleToCode[role] : 0;
            var user = new User
            {
                Username = candidate,
                PasswordHash = Md5Hex(pwd),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Role = roleCode.ToString(),
                CreatedBy = createdBy,
                CreatedDate = System.DateTime.UtcNow,
                LastUpdate = System.DateTime.UtcNow,
                Status = true,
                FirstLogin = 0  // Set FirstLogin to 0 for new users
            };

            var result = _userService.Insert(user);
            if (!result.IsSuccess) return (false, null, null, string.Join("; ", result.Errors));
            
            // Send email with credentials (async fire-and-forget)
            _ = Task.Run(async () => 
            {
                try
                {
                    await _emailService.SendNewUserCredentialsAsync(email, candidate, firstName, pwd);
                }
                catch (Exception ex)
                {
                    // Log error but don't fail user creation
                    System.Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            });
            
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

        public User? GetUserById(int userId)
        {
            var result = _userService.GetById(userId);
            return result.IsSuccess ? result.Value : null;
        }

        public async Task<(bool ok, string? error)> ChangePasswordFirstLogin(int userId, string newPassword)
        {
            var user = _userService.GetById(userId).Value;
            if (user == null)
                return (false, "Usuario no encontrado.");

            if (user.FirstLogin != 0)
                return (false, "Este usuario ya ha cambiado su contraseña inicial.");

            user.PasswordHash = Md5Hex(newPassword);
            user.FirstLogin = 1;
            user.LastUpdate = DateTime.UtcNow;

            var result = _userService.Update(user);
            if (!result.IsSuccess)
                return (false, string.Join("; ", result.Errors));

            return (true, null);
        }

        private static string Md5Hex(string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
