using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class EditModel : PageModel
    {
        private readonly IDbRespository<Establishment> _repository;
        private readonly IdProtector _idProtector;
        
        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public EditModel(IRepositoryFactory<Establishment> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepository();
            _idProtector = idProtector;
        }

        public IActionResult OnGet(string id)
        {
            int realId;
            try
            {
                realId = _idProtector.UnprotectInt(id);
            }
            catch
            {
                return RedirectToPage("./Error");
            }

            var entity = _repository.GetAll().FirstOrDefault(e => e.Id == realId);
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
            _repository.Update(Establishment);

            return RedirectToPage("./Index");
        }
    }
}