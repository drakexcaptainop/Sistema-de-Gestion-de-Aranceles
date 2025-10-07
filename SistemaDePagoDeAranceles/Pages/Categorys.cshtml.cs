using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages
{
    public class CategorysModel : PageModel
    {

        private readonly IDbRespository<Category> _categoryRepository;

        public CategorysModel(IDbRespository<Category> categoriaRepository)
        {
            _categoryRepository = categoriaRepository;
        }

        public IEnumerable<Category> Categorys { get; set; } = new List<Category>();

        public void OnGet()
        {
            Categorys = _categoryRepository.GetAll();
        }
    }
}
