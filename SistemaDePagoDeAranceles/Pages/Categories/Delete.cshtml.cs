using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using System.Linq;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IDbRespository<Category> _repository;

        public DeleteModel(IRepositoryFactory<Category> factory)
        {
            _repository = factory.CreateRepository();
        }

        [BindProperty]
        public Category Category { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var list = _repository.GetAll().ToList();
            Category = list.FirstOrDefault(c => c.Id == id);

            if (Category == null)
                return RedirectToPage("./Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            _repository.Delete(Category);
            return RedirectToPage("./Index");
        }
    }
}