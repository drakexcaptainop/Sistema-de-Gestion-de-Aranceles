using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using UIHost.Security;
using EstablishmentService.Domain.Models;
using Common.Domain.SharedPorts;

namespace UIHost.Pages.PersonInCharges
{
    public class CreateModel : PageModel
    {
        private readonly IRepositoryService<PersonInCharge> _repository;

        [BindProperty]
        public PersonInCharge Person { get; set; } = new();

        [BindProperty]
        [StringLength(50, ErrorMessage = "El segundo nombre no puede exceder 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z������������\s]*$", ErrorMessage = "El segundo nombre solo puede contener letras y espacios.")]
        public string? SecondName { get; set; }

        [BindProperty]
        [StringLength(50, ErrorMessage = "El segundo apellido no puede exceder 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z������������\s]*$", ErrorMessage = "El segundo apellido solo puede contener letras y espacios.")]
        public string? SecondLastName { get; set; }

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

            var fullFirstName = Person.FirstName.Trim();
            if (!string.IsNullOrWhiteSpace(SecondName))
            {
                fullFirstName += " " + SecondName.Trim();
            }

            var fullLastName = Person.LastName.Trim();
            if (!string.IsNullOrWhiteSpace(SecondLastName))
            {
                fullLastName += " " + SecondLastName.Trim();
            }

            Person.FirstName = fullFirstName;
            Person.LastName = fullLastName;

            Person.CreatedDate = DateTime.Now;
            Person.UpdateDate = DateTime.Now;
            Person.Status = true;

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
