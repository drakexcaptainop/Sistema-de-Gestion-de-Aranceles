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

            try
            {
                var (ok, userId, role, error, isFirstLogin) = _auth.ValidateLogin(Input.Username, Input.Password);
                if (!ok || userId == null) 
                { 
                    ModelState.AddModelError(string.Empty, error ?? "Credenciales inválidas."); 
                    return Page(); 
                }

                // Claims (role for [Authorize(Roles="Admin")] etc.). Include NameIdentifier claim with user id.
                var claims = new List<Claim>
                {
                    new (ClaimTypes.Name, Input.Username),
                    new (ClaimTypes.NameIdentifier, userId.ToString()),
                    new (ClaimTypes.Role, role ?? "User")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = Input.RememberMe });

                System.Console.WriteLine($"Login successful. FirstLogin: {isFirstLogin}, Role: {role}, UserId: {userId}");
                
                if (isFirstLogin)
                {
                    System.Console.WriteLine("Redirecting to ChangePassword");
                    return RedirectToPage("/ChangePassword");
                }

                if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    System.Console.WriteLine($"Redirecting to ReturnUrl: {ReturnUrl}");
                    return LocalRedirect(ReturnUrl);
                }

                System.Console.WriteLine("Redirecting to Index");
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Login error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error durante el inicio de sesión.");
                return Page();
            }

        }
    }
}
