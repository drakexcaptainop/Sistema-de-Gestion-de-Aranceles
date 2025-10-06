using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Models;

namespace SistemaDePagoDeAranceles.Pages
{
    public class CategoriasModel : PageModel
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriasModel(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IEnumerable<Categoria> Categorias { get; set; } = new List<Categoria>();

        public void OnGet()
        {
            Categorias = _categoriaRepository.GetAll();
        }
    }
}
