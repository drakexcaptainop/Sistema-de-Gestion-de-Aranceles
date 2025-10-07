using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class CreateModel : PageModel
    {
        private readonly PersonInChargeRepository _repository;

        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        public CreateModel(PersonInChargeRepository repository)
        {
            _repository = repository;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Person)}");
                return Page();
            }

            Person.RegisterDate = DateTime.Now;
            Person.UpdateDate = DateTime.Now;
            Person.Status = true;
            Person.CreatedBy = 1;

            _repository.Insert(Person);
            return RedirectToPage("./Index");
        }
    }
}