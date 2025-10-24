using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Helpers;
using SistemaDePagoDeAranceles.Domain.Common;


namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;
        private readonly IdProtector _idProtector;
        public Result<Category> GetAllResult { get; set; }
        public EditModel(IRepositoryServiceFactory<Category> factory, IdProtector idProtector)
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

            var result = _repository.GetAll();

            if (result.IsFailure)
            {   
                return NotFound(result.Errors.FirstOrDefault());
            }
            
            var list = result.Value;
            Category = list.FirstOrDefault(c => c.Id == realId);

            if (Category == null)
                return RedirectToPage("./Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Category)}");
                return Page();
            }

            Category.LastUpdate = DateTime.Now;
            var result = _repository.Update(Category);
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