using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserManagementService.Application.Helpers;
using UserManagementService.Domain.Ports;

namespace UIHost.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAuthService _authService;

        public ChangePasswordModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        [Required(ErrorMessage = "La contraseña actual es requerida")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public bool IsFirstLogin { get; private set; }

        public IActionResult OnGet()
        {
            var userId = User.GetUserId();
            if (userId == null) return RedirectToPage("/Login");

            var user = _authService.GetUserById(userId.Value);
            if (user == null) return RedirectToPage("/Login");

            IsFirstLogin = (user.FirstLogin == 0);

            ViewData["HideSidebar"] = IsFirstLogin;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.GetUserId();
            if (userId == null) return RedirectToPage("/Login");

            var user = _authService.GetUserById(userId.Value);
            if (user == null) return RedirectToPage("/Login");

            IsFirstLogin = (user.FirstLogin == 0);
            ViewData["HideSidebar"] = IsFirstLogin;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            (bool ok, string? error) result;

            if (user.FirstLogin == 0)
            {
                result = await _authService.ChangePasswordFirstLogin(userId.Value, CurrentPassword, NewPassword);
            }
            else
            {
                result = await _authService.ChangePassword(userId.Value, CurrentPassword, NewPassword);
            }

            if (!result.ok)
            {
                if (result.error?.Contains("actual", StringComparison.OrdinalIgnoreCase) == true)
                    ModelState.AddModelError(nameof(CurrentPassword), result.error);
                else
                    ModelState.AddModelError(string.Empty, result.error ?? "Error al cambiar la contraseña");
                return Page();
            }

            return RedirectToPage("/Index");
        }

    }
}