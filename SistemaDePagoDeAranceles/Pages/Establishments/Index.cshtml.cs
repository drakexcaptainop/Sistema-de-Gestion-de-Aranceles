using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class IndexModel : PageModel
    {
        private readonly IDbRespository<Establishment> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<Establishment> Establishments { get; set; } = new();

        public IndexModel(IRepositoryFactory<Establishment> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepository();
            _idProtector =  idProtector;
        }

        public void OnGet()
        {
            Establishments = _repository.GetAll().Where(establishment =>  establishment.Active).ToList();
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

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}