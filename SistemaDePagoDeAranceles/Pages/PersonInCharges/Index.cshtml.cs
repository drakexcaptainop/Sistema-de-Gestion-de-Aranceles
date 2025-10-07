using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class IndexModel : PageModel
    {
        private readonly IDbRespository<PersonInCharge> _repository;

        public List<PersonInCharge> Persons { get; set; } = new();

        public IndexModel(IRepositoryFactory<PersonInCharge> factory)
        {
            _repository = factory.CreateRepository();
        }

        public void OnGet()
        {
            Persons = _repository.GetAll().ToList();
        }
    }
}