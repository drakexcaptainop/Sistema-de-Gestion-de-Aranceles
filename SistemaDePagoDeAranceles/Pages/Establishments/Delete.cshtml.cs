using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class DeleteModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public DeleteModel(IRepositoryServiceFactory<Establishment> factory, IdProtector idProtector)
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
                return RedirectToPage("./Index");
            }

            var entity = _repository.GetAll().FirstOrDefault(e => e.Id == realId);
            if (entity == null)
                return RedirectToPage("Index");

            Establishment = entity;
            return Page();
        }

        public IActionResult OnPost()
        {
            _repository.Delete(Establishment);  
            return RedirectToPage("./Index");
        }
    }
}