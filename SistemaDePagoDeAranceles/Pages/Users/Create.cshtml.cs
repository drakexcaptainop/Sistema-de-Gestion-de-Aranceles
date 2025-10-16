using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;
using System.ComponentModel.DataAnnotations;

namespace SistemaDePagoDeAranceles.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IAuthService _auth;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IAuthService auth, ILogger<CreateModel> logger)
        {
            _auth = auth;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();
        
        public string? GeneratedUsername { get; set; }
        public string? GeneratedPassword { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string FirstName { get; set; } = string.Empty;
            
            [Required(ErrorMessage = "El apellido es obligatorio")]
            public string LastName { get; set; } = string.Empty;
            
            [Required(ErrorMessage = "El email es obligatorio")]
            [EmailAddress(ErrorMessage = "Email inválido")]
            public string Email { get; set; } = string.Empty;
            
            [Required(ErrorMessage = "El rol es obligatorio")]
            public string Role { get; set; } = "Contador";
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the id of the admin creating this user from the authenticated principal
            int adminId = 0;
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedId))
                adminId = parsedId;

            var (ok, usern, passw, err) = _auth.RegisterUser(
                Input.FirstName,
                Input.LastName,
                Input.Email,
                Input.Role,
                adminId
            );

            if (!ok)
            {
                ModelState.AddModelError(string.Empty, err ?? "No se pudo registrar el usuario.");
                return Page();
            }

            GeneratedUsername = usern;
            GeneratedPassword = passw;
            
            // Log generated credentials
            _logger.LogInformation("New user created: {Username} / {Password}", usern, passw);
            Console.WriteLine($"New user created: {usern} / {passw}");
            
            return Page();
        }
    }
}
