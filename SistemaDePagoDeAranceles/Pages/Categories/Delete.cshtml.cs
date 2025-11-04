using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Application.Services;
using System.Linq;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;
        private readonly IdProtector _idProtector;

        public DeleteModel(IRepositoryServiceFactory<Category> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        [BindProperty]
        public Category Category { get; set; } = new();

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
                return RedirectToPage("./NotFound");
            }

            var list = result.Value;
            Category = list.FirstOrDefault(c => c.Id == realId);

            if (Category == null)
                return RedirectToPage("./Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            var result = _repository.Delete(Category);
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