using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Pages.Establishments
{
    public class CreateModel : PageModel
    {
        private readonly IDbRespository<Establishment> _repository;
        private readonly IDbRespository<PersonInCharge> _personRepository;

        [BindProperty]
        public Establishment Establishment { get; set; } = new();
        public List<PersonInCharge> PersonsInCharge { get; set; } = new();

        public CreateModel(IRepositoryFactory<Establishment> factory, IRepositoryFactory<PersonInCharge> personFactory)
        {
            _repository = factory.CreateRepository();
            _personRepository = personFactory.CreateRepository();
        }

        public void OnGet()
        {
            PersonsInCharge = _personRepository.GetAll().ToList();
        }

        public IActionResult OnPost()
        {
            Establishment.RegisterDate = DateTime.Now;
            Establishment.LastUpdate = DateTime.Now;
            Establishment.Active = true;
            Establishment.CreatedBy = 1;
            
            if (!ModelState.IsValid)
            {
                var result1 = _repository.Insert(Establishment);
                Console.WriteLine($"[DEBUG] Insertando: {System.Text.Json.JsonSerializer.Serialize(Establishment)}");
                Console.WriteLine($"[DEBUG] Resultado de inserción: {result1}");
                return Page();
            }
            var result = _repository.Insert(Establishment);

            if (result > 0)
            {
                return RedirectToPage("./Index");
            }
            ModelState.AddModelError(string.Empty, "Error al registrar el establecimiento.");
            return Page();
        }

    }
}