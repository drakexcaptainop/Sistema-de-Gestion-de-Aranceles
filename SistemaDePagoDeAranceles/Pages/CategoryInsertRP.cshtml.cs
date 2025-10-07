using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages;

public class CategoryInsertRP : PageModel
{
    [BindProperty]
    public Category Category { get; set; } = new Category();
    private readonly IDbRespository<Category> categoryRepository;
    public CategoryInsertRP(RepositoryFactory<Category> factory)
    {
        categoryRepository = factory.CreateRepository();
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        categoryRepository.Insert(Category);
        return Redirect("./Categorys");
    }
}