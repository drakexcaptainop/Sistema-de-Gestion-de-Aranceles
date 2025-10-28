using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Common;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Category> Categories { get; set; } = new();

        public IndexModel(IRepositoryServiceFactory<Category> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            LoadCategories();
        }

        public void OnPostSearch()
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            Result<IEnumerable<Category>> result;

            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                result = _repository.GetAll();
            }
            else
            {
                result = _repository.Search(SearchTerm);
            }

            Categories = result.IsSuccess && result.Value != null
                ? result.Value.ToList()
                : new List<Category>();
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}
