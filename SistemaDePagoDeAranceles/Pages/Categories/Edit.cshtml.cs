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
        private readonly IDbRespository<Category> _repository;

        public EditModel(IRepositoryFactory<Category> factory)
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
            Category.LastUpdate = DateTime.Now;
            Category.CreatedBy = 1;
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Category)}");
                return Page();
            }

            _repository.Update(Category);

            return RedirectToPage("./Index");
        }
    }
}