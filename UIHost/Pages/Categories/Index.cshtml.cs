using Microsoft.AspNetCore.Mvc.RazorPages;

using UIHost.Security;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using Microsoft.AspNetCore.Mvc;
using TariffingService.Domain.Models;

namespace UIHost.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public Result<IEnumerable<Category>> ResultCategoryGetAll { get; set; }

        public List<Category> Categories { get; set; } = new();

        public IndexModel(IRepositoryServiceFactory<Category> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            ResultCategoryGetAll = _repository.GetAll();
        }

        public void OnPostSearch()
        {
            ResultCategoryGetAll = string.IsNullOrWhiteSpace(SearchTerm) ? _repository.GetAll() : _repository.Search(SearchTerm);
        }
        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}