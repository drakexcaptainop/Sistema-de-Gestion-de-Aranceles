using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages
{
    public class CategorysModel : PageModel
    {

        private readonly RepositoryFactory<Category> _categoryRepository;

        public CategorysModel(RepositoryFactory<Category> categoriaRepository)
        {
            _categoryRepository = categoriaRepository;
        }

        public IEnumerable<Category> Categorys { get; set; } = new List<Category>();

        public void OnGet()
        {
            Categorys = _categoryRepository.CreateRepository().GetAll();
        }
    }
}
