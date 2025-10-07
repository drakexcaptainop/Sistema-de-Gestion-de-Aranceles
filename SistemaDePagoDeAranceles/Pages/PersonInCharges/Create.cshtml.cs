using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class CreateModel : PageModel
    {
        private readonly IDbRespository<PersonInCharge> _repository;

        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        public CreateModel(IRepositoryFactory<PersonInCharge> factory)
        {
            _repository = factory.CreateRepository();
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Person.RegisterDate = DateTime.Now;
            Person.UpdateDate = DateTime.Now;
            Person.Status = true;
            Person.CreatedBy = 1;

            _repository.Insert(Person);
            return RedirectToPage("./Index");
        }
    }
}