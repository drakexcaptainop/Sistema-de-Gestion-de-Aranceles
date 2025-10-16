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

        public RegisterModel(IAuthService auth) { _auth = auth; }

        [BindProperty] public InputModel Input { get; set; } = new();
        public string? GeneratedUsername { get; set; }
        public string? GeneratedPassword { get; set; }

        public class InputModel
        {
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required] public string LastName { get; set; } = string.Empty;
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            [Required] public string Role { get; set; } = "Contador"; // Admin | Contador
        }

        public void OnGet() {}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            int adminId = 1; // TODO: replace with the logged-in Admin user id if available

            var (ok, usern, passw, err) = _auth.RegisterUser(Input.FirstName, Input.LastName, Input.Email, Input.Role, adminId);
            if (!ok) { ModelState.AddModelError(string.Empty, err ?? "No se pudo registrar."); return Page(); }

            GeneratedUsername = usern;
            GeneratedPassword = passw;
            TempData["NewUser"] = $"Usuario: {usern} / Contraseña: {passw}";
            return RedirectToPage("/Register");
        }
    }
}
