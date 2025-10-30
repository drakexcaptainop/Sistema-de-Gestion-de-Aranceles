using UIHost.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using EstablishmentService.Domain.Models;
using UserManagementService.Application.Helpers;

namespace UIHost.Pages.Establishments
{
    public class EditModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IRepositoryService<PersonInCharge> _personRepository;
        private readonly IdProtector _idProtector;

        public List<PersonInCharge> PersonsInCharge { get; set; } = new();

        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public Result<IEnumerable<PersonInCharge>> ResultGetAllPersonInCharge { get; set; }

        public EditModel(
            IRepositoryServiceFactory<Establishment> factory,
            IRepositoryServiceFactory<PersonInCharge> personFactory,
            IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _personRepository = personFactory.CreateRepositoryService();
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

            Establishment = entity;

            LoadPersonsInCharge();

            Console.WriteLine(Establishment.PersonInChargeId);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[DEBUG] Actualizando: {System.Text.Json.JsonSerializer.Serialize(Establishment)}");

                LoadPersonsInCharge();

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

            LoadPersonsInCharge();
            return Page();
        }

        private void LoadPersonsInCharge()
        {
            ResultGetAllPersonInCharge = _personRepository.GetAll();

            if (ResultGetAllPersonInCharge.IsSuccess && ResultGetAllPersonInCharge.Value != null)
            {
                PersonsInCharge = ResultGetAllPersonInCharge.Value
                    .Where(personInCharge => personInCharge.Status)
                    .ToList();
            }
        }
    }
}
