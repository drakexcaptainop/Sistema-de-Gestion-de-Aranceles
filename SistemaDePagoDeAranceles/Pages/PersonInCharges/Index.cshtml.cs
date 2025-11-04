using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Common;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<PersonInCharge> _repository;
        private readonly IdProtector _idProtector;
        public List<PersonInCharge> Persons { get; set; } = new();
        public Result<IEnumerable<PersonInCharge>> ResultGetAllPersonInCharge { get; set; }

        public IndexModel(IRepositoryServiceFactory<PersonInCharge> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            ResultGetAllPersonInCharge = _repository.GetAll();
            Persons = ResultGetAllPersonInCharge.Value?.ToList() ?? new List<PersonInCharge>();
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}