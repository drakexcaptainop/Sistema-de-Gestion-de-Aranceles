using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class CreateModel : PageModel
    {
        private readonly EstablishmentRepository _repository;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public CreateModel(EstablishmentRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Establishment.RegisterDate = DateTime.Now;
            Establishment.LastUpdate = DateTime.Now;
            Establishment.SanitaryLicenseExpiry= DateTime.Now;
            Establishment.Active = true;
            Establishment.CreatedBy = 1;
            Establishment.PersonInChargeId = 1;
            
            if (!ModelState.IsValid)
            {
                var result1 = _repository.Insert(Establishment);
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Establishment)}");
                Console.WriteLine($"[DEBUG] Resultado de inserción: {result1}");
                return Page();
            }
            var result = _repository.Insert(Establishment);

            if (result > 0)
            {
                return RedirectToPage("./Index");
            }
            ModelState.AddModelError(string.Empty, "Error al registrar el establecimiento.");
            return Page();
        }

    }
}