using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SistemaDePagoDeAranceles.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        [FromQuery(Name = "ReturnUrl")]
        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required, Display(Name = "Username")]
            public string Username { get; set; } = "";

            [Required, DataType(DataType.Password)]
            public string Password { get; set; } = "";

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // TODO: replace with your real user lookup + hashed password check.
            var isValid = (Input.Username == "admin" && Input.Password == "pass123");

            if (!isValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            var claims = new List<Claim>
        {
            new(ClaimTypes.Name, Input.Username),
            new(ClaimTypes.Role, "User")
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = Input.RememberMe
                });

            if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                return LocalRedirect(ReturnUrl);

            return RedirectToPage("/Index");
        }
    }
}
