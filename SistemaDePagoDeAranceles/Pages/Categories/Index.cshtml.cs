using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Factory;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IDbRespository<Category> _repository;

        public List<Category> Categories { get; set; } = new();

        public IndexModel(IRepositoryFactory<Category> factory)
        {
            _repository = factory.CreateRepository();
        }

        public void OnGet()
        {
            Categories = _repository.GetAll().ToList();
        }
    }
}