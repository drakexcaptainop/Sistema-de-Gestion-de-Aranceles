using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class DeleteModel : PageModel
    {
        private readonly IDbRespository<Establishment> _repository;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();

        public DeleteModel(IRepositoryFactory<Establishment> factory)
        {
            _repository = factory.CreateRepository();
        }

        public IActionResult OnGet(int id)
        {
            var entity = _repository.GetAll().FirstOrDefault(e => e.Id == id);
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