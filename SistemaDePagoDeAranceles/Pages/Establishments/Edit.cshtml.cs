using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class EditModel : PageModel
    {
        private readonly EstablishmentRepository _repository;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public EditModel(EstablishmentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(int id)
        {
            var entity = _repository.GetAll().FirstOrDefault(e => e.Id == id);
            Console.WriteLine(entity.PersonInChargeId);
            if (entity == null)
                return RedirectToPage("./Index");

            Establishment = entity;
            Console.WriteLine(Establishment.PersonInChargeId);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Establishment)}");
                return Page();
            }

            Establishment.LastUpdate = DateTime.Now;
            _repository.Update(Establishment);

            return RedirectToPage("./Index");
        }
    }
}