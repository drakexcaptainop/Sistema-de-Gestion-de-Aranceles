using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;


namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class CreateModel : PageModel
    {
        private readonly IRepositoryService<PersonInCharge> _repository;

        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        public CreateModel(IRepositoryServiceFactory<PersonInCharge> factory)
        {
            _repository = factory.CreateRepositoryService();
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
            // use authenticated user's id as CreatedBy
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedCreatorId))
                Person.CreatedBy = parsedCreatorId;

            var result = _repository.Insert(Person);
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