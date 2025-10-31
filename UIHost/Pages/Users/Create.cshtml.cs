using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using UserManagementService.Domain.Ports;

namespace UIHost.Pages.Users
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

        [BindProperty]
        [StringLength(50, ErrorMessage = "El segundo nombre no puede exceder 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "El segundo nombre solo puede contener letras y espacios.")]
        public string? SecondName { get; set; }

        [BindProperty]
        [StringLength(50, ErrorMessage = "El segundo apellido no puede exceder 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "El segundo apellido solo puede contener letras y espacios.")]
        public string? SecondLastName { get; set; }

        public string? GeneratedUsername { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "El apellido es obligatorio.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido debe tener entre 3 y 50 caracteres.")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
            [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres.")]
            [EmailAddress(ErrorMessage = "Correo electrónico inválido.")]
            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]{2,})+$",
                ErrorMessage = "El correo electrónico debe contener al menos un punto (.) en el dominio.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "El rol es obligatorio.")]
            public string Role { get; set; } = "Contador";
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int adminId = 0;
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedId))
                adminId = parsedId;

            var fullFirstName = Input.FirstName.Trim();
            if (!string.IsNullOrWhiteSpace(SecondName))
            {
                fullFirstName += " " + SecondName.Trim();
            }

            var fullLastName = Input.LastName.Trim();
            if (!string.IsNullOrWhiteSpace(SecondLastName))
            {
                fullLastName += " " + SecondLastName.Trim();
            }

            var (ok, usern, passw, err) = _auth.RegisterUser(
                fullFirstName,      
                fullLastName,       
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

            _logger.LogInformation("New user created: {Username}. Credentials sent to: {Email}", usern, Input.Email);

            return Page();
        }
    }
}
