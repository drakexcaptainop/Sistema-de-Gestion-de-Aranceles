using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Application.Helpers;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;

namespace SistemaDePagoDeAranceles.Pages
{
    public class MyProfileModel : PageModel
    {
        private readonly IAuthService _authService;

        public MyProfileModel(IAuthService authService)
        {
            _authService = authService;
        }

        public string Username { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string RoleName { get; private set; } = string.Empty;
        public bool IsFirstLogin { get; private set; }


        public IActionResult OnGet()
        {
            var userId = User.GetUserId();
            if (userId == null) return RedirectToPage("/Login");

            var user = _authService.GetUserById(userId.Value);
            if (user == null) return RedirectToPage("/Login");

            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            RoleName = MapRole(user.Role);

            return Page();
        }

        private static string MapRole(string? roleCodeStr)
        {
            if (string.IsNullOrWhiteSpace(roleCodeStr)) return "Desconocido";
            if (!int.TryParse(roleCodeStr, out var code)) return "Desconocido";
            return code switch
            {
                1 => "Administrador",
                2 => "Contador",
                _ => "Desconocido"
            };
        }


    }
}
