using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Application.Helpers;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class EditModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IdProtector _idProtector;
        
        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public EditModel(IRepositoryServiceFactory<Establishment> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
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
            Establishment.LastUpdate = DateTime.Now;
            var editorId = User.GetUserId();
            var result = _repository.Update(Establishment);

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return Page();
        }
    }
}