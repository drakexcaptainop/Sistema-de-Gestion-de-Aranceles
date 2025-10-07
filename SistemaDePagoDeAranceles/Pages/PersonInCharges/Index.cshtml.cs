using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class IndexModel : PageModel
    {
        private readonly PersonInChargeRepository _repository;

        public List<PersonInCharge> Persons { get; set; } = new();

        public IndexModel(PersonInChargeRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
            Persons = _repository.GetAll().ToList();
        }
    }
}