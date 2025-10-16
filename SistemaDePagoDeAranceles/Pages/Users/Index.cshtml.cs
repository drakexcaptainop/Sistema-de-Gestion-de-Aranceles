using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

namespace SistemaDePagoDeAranceles.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<User> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<User> Users { get; set; } = new();

        public IndexModel(IRepositoryServiceFactory<User> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            Users = _repository.GetAll().Where(user => user.Status).ToList();
        }

        public void OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                Users = _repository.GetAll().Where(user => user.Status).ToList();
            }
            else
            {
                Users = _repository.Search(SearchTerm).Where(user => user.Status).ToList();
            }
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}
