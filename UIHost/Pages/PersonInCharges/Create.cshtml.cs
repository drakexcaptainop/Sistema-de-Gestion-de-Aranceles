using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UIHost.Security;
using EstablishmentService.Domain.Models;
using Common.Domain.SharedPorts;
using Common.Infrastructure.Logger;


namespace UIHost.Pages.PersonInCharges
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
                AuditHelper.LogUserAction(User, "CREATE", nameof(Person), $"Se creó el siguiente encargado {Person.FirstName}");
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Person)}");
                return Page();
            }

            Person.CreatedDate = DateTime.Now;
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