using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class IndexModel : PageModel
    {
        private readonly IDbRespository<Establishment> _repository;

        public List<Establishment> Establishments { get; set; } = new();

        public IndexModel(IRepositoryFactory<Establishment> factory)
        {
            _repository = factory.CreateRepository();
        }

        public void OnGet()
        {
            Establishments = _repository.GetAll().ToList();
        }
    }
}