using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SistemaDePagoDeAranceles.Application.Helpers;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;

namespace SistemaDePagoDeAranceles.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAuthService _authService;

        public ChangePasswordModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        public IActionResult OnGet()
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var user = _authService.GetUserById(userId.Value);
            if (user == null || user.FirstLogin != 0)
            {
                return RedirectToPage("/Index");
            }

            ViewData["HideSidebar"] = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.GetUserId();
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var result = await _authService.ChangePasswordFirstLogin(userId.Value, NewPassword);
            if (!result.ok)
            {
                ModelState.AddModelError(string.Empty, result.error ?? "Error al cambiar la contraseña");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}