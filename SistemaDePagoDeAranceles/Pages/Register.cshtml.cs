using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;
using System.ComponentModel.DataAnnotations;

namespace SistemaDePagoDeAranceles.Pages
{
    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _auth;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(IAuthService auth, ILogger<RegisterModel> logger) { _auth = auth; _logger = logger; }

        [BindProperty] public InputModel Input { get; set; } = new();
        public string? GeneratedUsername { get; set; }
        public string? GeneratedPassword { get; set; }

        public class InputModel
        {
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required] public string LastName { get; set; } = string.Empty;
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            [Required] public string Role { get; set; } = "Contador";
        }

        public void OnGet() {}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            // Get the id of the admin creating this user from the authenticated principal
            int adminId = 0;
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedId)) adminId = parsedId;

            var (ok, usern, passw, err) = _auth.RegisterUser(Input.FirstName, Input.LastName, Input.Email, Input.Role, adminId);
            if (!ok) { ModelState.AddModelError(string.Empty, err ?? "No se pudo registrar."); return Page(); }

            GeneratedUsername = usern;
            GeneratedPassword = passw;
            // Print generated credentials to console and log for now; later replace with an email sender
            _logger.LogInformation("New user created: {Username} / {Password}", usern, passw);
            System.Console.WriteLine($"New user created: {usern} / {passw}");
            TempData["NewUser"] = $"Usuario: {usern} / Contraseña: {passw}";
            return RedirectToPage("/Register");
        }
    }
}
