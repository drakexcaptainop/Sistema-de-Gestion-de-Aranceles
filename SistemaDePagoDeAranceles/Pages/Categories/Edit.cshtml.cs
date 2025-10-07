using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using System;
using System.Linq;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly CategoryRepository _repository;

        public EditModel(CategoryRepositoryCreator factory)
        {
            _repository = (CategoryRepository)factory.CreateRepository();
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
            if (!ModelState.IsValid)
                return Page();

            Category.LastUpdate = DateTime.Now;

            _repository.Update(Category);

            return RedirectToPage("./Index");
        }
    }
}