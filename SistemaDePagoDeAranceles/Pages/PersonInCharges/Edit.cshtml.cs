using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Application.Helpers;

namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class EditModel : PageModel
    {
        private readonly IRepositoryService<PersonInCharge> _repository;
        private readonly IdProtector _idProtector;
        
        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        public EditModel(IRepositoryServiceFactory<PersonInCharge> factory, IdProtector idProtector)
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
                return RedirectToPage("../Error");
            }

            var result = _repository.GetAll();
            if (result.IsFailure)
            {
                return RedirectToPage("Index");
            }
            var entity = result.Value.FirstOrDefault(e => e.Id == realId);
            if (entity == null)
                return RedirectToPage("./Index");

            Person = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Person)}");
                return Page();
            }
            
            Person.UpdateDate = DateTime.Now;
            var result = _repository.Update(Person);
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