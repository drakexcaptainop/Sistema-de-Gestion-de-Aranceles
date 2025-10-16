using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<Establishment> Establishments { get; set; } = new();

        public IndexModel(IRepositoryServiceFactory<Establishment> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
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