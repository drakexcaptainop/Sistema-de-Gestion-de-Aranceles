using Microsoft.AspNetCore.Mvc;
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
            Persons = _repository.GetAll().Where(personInCharge => personInCharge.Status).ToList();
        }
        
        public IActionResult OnPostDelete(int id)
        {
            var personInCharge = _repository.GetAll().ToList().FirstOrDefault(p => p.Id == id);
            
            _repository.Delete(personInCharge);
            return RedirectToPage();
        }
    }
}