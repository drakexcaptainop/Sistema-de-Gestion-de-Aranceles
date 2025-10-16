using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Common;

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
        public Result<IEnumerable<User>> ResultGetAllUser { get; set; }

        public IndexModel(IRepositoryServiceFactory<User> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            ResultGetAllUser = _repository.GetAll();
        }

        public void OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                ResultGetAllUser = _repository.GetAll();
            }
            else
            {
                ResultGetAllUser = _repository.Search(SearchTerm);
            }
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}
