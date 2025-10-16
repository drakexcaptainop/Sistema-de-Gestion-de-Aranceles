using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

namespace SistemaDePagoDeAranceles.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IRepositoryService<User> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public new User User { get; set; } = new();

        public EditModel(IRepositoryServiceFactory<User> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public IActionResult OnGet(string id)
        {
            int realId;
            try
            {
                realId = _idProtector.UnprotectInt(id);
            }
            catch
            {
                return RedirectToPage("./Index");
            }

            var result = _repository.GetAll();
            if (result.IsFailure)
            {
                return NotFound();
            }
            var entity = result.Value.FirstOrDefault(u => u.Id == realId);
            if (entity == null)
                return RedirectToPage("./Index");

            User = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User.LastUpdate = DateTime.UtcNow;
            var result = _repository.Update(User);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                return RedirectToPage("./Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            TempData["ErrorMessage"] = "Error al actualizar el usuario.";
            return Page();
        }
    }
}
