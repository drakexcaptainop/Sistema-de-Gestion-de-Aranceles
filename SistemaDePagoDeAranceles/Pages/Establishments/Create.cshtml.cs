using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Common;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class CreateModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IRepositoryService<PersonInCharge> _personRepository;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();
        public List<PersonInCharge> PersonsInCharge { get; set; } = new();
        
        public Result<IEnumerable<PersonInCharge>> ResultGetAllPersonInCharge { get; set; }

        public CreateModel(IRepositoryServiceFactory<Establishment> factory, IRepositoryServiceFactory<PersonInCharge> personFactory)
        {
            _repository = factory.CreateRepositoryService();
            _personRepository = personFactory.CreateRepositoryService();
        }

        public void OnGet()
        {
            ResultGetAllPersonInCharge = _personRepository.GetAll();
            if (ResultGetAllPersonInCharge.IsSuccess)
            {
                PersonsInCharge = ResultGetAllPersonInCharge.Value.Where(personInCharge => personInCharge.Status).ToList();
            }
        }

        public IActionResult OnPost()
        {
            Establishment.RegisterDate = DateTime.Now;
            Establishment.LastUpdate = DateTime.Now;
            Establishment.BusinessName = ".";
            Establishment.Active = true;
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedCreatorId))
                Establishment.CreatedBy = parsedCreatorId;
            
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }
            
            Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Establishment)}");
            var result = _repository.Insert(Establishment);
            Console.WriteLine($"[DEBUG] Resultado de inserción: {System.Text.Json.JsonSerializer.Serialize(result)}");

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return RedirectToPage();
        }

    }
}