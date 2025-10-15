using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class IndexModel : PageModel
    {
        private readonly EstablishmentRepository _repository;

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<Establishment> Establishments { get; set; } = new();

        public IndexModel(EstablishmentRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
            Establishments = _repository.GetAll().ToList();
        }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                Establishments = _repository.GetAll().ToList();
            }
            else
            {
                Establishments = _repository.Search(SearchTerm).ToList();
            }
        }

    }
}