using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;

        public CreateModel(IRepositoryServiceFactory<Category> factory)
        {
            _repository = factory.CreateRepositoryService();
        }
        
        [BindProperty]
        public Category Category { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            Category.CreatedDate = DateTime.Now;
            Category.LastUpdate = DateTime.Now;
            // use authenticated user's id as CreatedBy
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedCreatorId))
                Category.CreatedBy = parsedCreatorId;
            Category.Status = true;
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Category)}");
                return Page();
            }
            var result = _repository.Insert(Category);
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