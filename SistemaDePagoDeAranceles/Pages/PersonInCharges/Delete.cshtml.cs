using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class DeleteModel : PageModel
    {
        private readonly PersonInChargeRepository _repository;

        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        public DeleteModel(PersonInChargeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(int id)
        {
            var entity = _repository.GetAll().FirstOrDefault(e => e.Id == id);
            if (entity == null)
                return RedirectToPage("Index");

            Person = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            _repository.Delete(Person);
            return RedirectToPage("Index");
        }
    }
}