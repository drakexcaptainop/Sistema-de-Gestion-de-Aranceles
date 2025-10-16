using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class IndexModel : PageModel
    {
        private readonly IDbRespository<PersonInCharge> _repository;
        private readonly IdProtector _idProtector;
        public List<PersonInCharge> Persons { get; set; } = new();

        public IndexModel(IRepositoryFactory<PersonInCharge> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepository();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            Persons = _repository.GetAll().ToList();
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}