using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using Common.Infrastructure.Logger;
using EstablishmentService.Domain.Models;

namespace UIHost.Pages.Establishments
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
            var idClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var parsedCreatorId))
                Establishment.CreatedBy = parsedCreatorId;

            if (!ModelState.IsValid)
            {
                AuditHelper.LogUserAction(User, "CREATE", nameof(Establishment), $"Se creó el siguiente establecimiento {Establishment.Name}");
                ResultGetAllPersonInCharge = _personRepository.GetAll();
                if (ResultGetAllPersonInCharge.IsSuccess)
                {
                    PersonsInCharge = ResultGetAllPersonInCharge.Value.Where(personInCharge => personInCharge.Status).ToList();
                }
                return Page();
            }

            Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Establishment)}");
            var result = _repository.Insert(Establishment);
            Console.WriteLine($"[DEBUG] Resultado de inserción: {System.Text.Json.JsonSerializer.Serialize(result)}");

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

    }
}