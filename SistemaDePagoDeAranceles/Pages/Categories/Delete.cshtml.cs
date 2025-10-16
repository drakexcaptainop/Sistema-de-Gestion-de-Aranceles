using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using System.Linq;
using SistemaDePagoDeAranceles.Application.Services;

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
                return RedirectToPage("../Error");
            }
            
            var entity = _repository.GetAll().FirstOrDefault(c => c.Id == realId);
            if (entity == null)
            {
                return RedirectToPage("./Index");
            }

            Category = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            _repository.Delete(Category);
            return RedirectToPage("./Index");
        }
    }
}