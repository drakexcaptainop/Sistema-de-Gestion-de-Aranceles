using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class IndexModel : PageModel
    {
        private readonly EstablishmentRepository _repository;

        public List<Establishment> Establishments { get; set; } = new();

        public IndexModel(EstablishmentRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
            Establishments = _repository.GetAll().ToList();
        }
    }
}