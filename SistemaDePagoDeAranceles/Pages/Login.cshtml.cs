using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SistemaDePagoDeAranceles.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _auth;

        public LoginModel(IAuthService auth)
        {
            _auth = auth;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        [FromQuery(Name = "ReturnUrl")]
        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required] public string Username { get; set; } = string.Empty;
            [Required, DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
            public bool RememberMe { get; set; }
        }

        public void OnGet() {}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var (ok, userId, role, error) = _auth.ValidateLogin(Input.Username, Input.Password);
            if (!ok) { ModelState.AddModelError(string.Empty, error ?? "Credenciales inválidas."); return Page(); }

            // Claims (role for [Authorize(Roles="Admin")] etc.). Include NameIdentifier claim with user id.
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, Input.Username),
                new (ClaimTypes.NameIdentifier, userId?.ToString() ?? string.Empty),
                new (ClaimTypes.Role, role ?? "User")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = Input.RememberMe });

            // Session usage removed — layout reads roles from claims

            if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                return LocalRedirect(ReturnUrl);

            return RedirectToPage("/Index");
        }
    }
}
