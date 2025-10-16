using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;


namespace SistemaDePagoDeAranceles.Pages.PersonInCharges
{
    public class DeleteModel : PageModel
    {
        private readonly IRepositoryService<PersonInCharge> _repository;

        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        public DeleteModel(IRepositoryServiceFactory<PersonInCharge> factory)
        {
            _repository = factory.CreateRepositoryService();
        }

        public IActionResult OnGet(int id)
        {
            var entity = _repository.GetAll().FirstOrDefault(e => e.Id == id);
            if (entity == null)
                return RedirectToPage("./Index");

            Person = entity;
            return Page();
        }
        public IActionResult OnPost()
        {
            _repository.Delete(Person);
            return RedirectToPage("./Index");
        }

    }
}