using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Common;
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
        public Result<IEnumerable<Establishment>> EstablishmentsGetAllResult { get; set; } 

        public IndexModel(IRepositoryServiceFactory<Establishment> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector =  idProtector;
        }

        public void OnGet()
        {
            EstablishmentsGetAllResult = _repository.GetAll();
            Establishments = EstablishmentsGetAllResult.Value?.ToList() ?? new List<Establishment>();
        }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                EstablishmentsGetAllResult = _repository.GetAll();
            }
            else
            {
                EstablishmentsGetAllResult = _repository.Search(SearchTerm);
            }
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}