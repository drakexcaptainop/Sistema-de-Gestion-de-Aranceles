using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class DeleteModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public DeleteModel(IRepositoryServiceFactory<Establishment> factory, IdProtector idProtector)
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
                return NotFound(result.Errors.FirstOrDefault());
            }

            var entity = result.Value.FirstOrDefault(e => e.Id == realId);
            if (entity == null)
                return RedirectToPage("Index");

            Establishment = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            var result = _repository.Delete(Establishment);
            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return Page();
        }
    }
}