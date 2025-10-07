using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using System;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly CategoryRepository _repository;

        public CreateModel(CategoryRepositoryCreator factory)
        {
            _repository = (CategoryRepository)factory.CreateRepository();
        }

        [BindProperty]
        public Category Category { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            Category.RegisterDate = DateTime.Now;
            Category.LastUpdate = DateTime.Now;
            Category.CreatedBy = 1;
            Category.Active = true;
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Category)}");
                return Page();
            }
            _repository.Insert(Category);

            return RedirectToPage("./Index");
        }
    }
}