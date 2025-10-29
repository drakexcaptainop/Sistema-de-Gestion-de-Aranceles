using Microsoft.AspNetCore.Mvc.RazorPages;

using UIHost.Security;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using TariffingService.Domain.Models;

namespace UIHost.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;
        private readonly IdProtector _idProtector;
        public List<Category> Categories { get; set; } = new();
        public Result<IEnumerable<Category>> CategoriesResult { get; set; }

        public IndexModel(IRepositoryServiceFactory<Category> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            CategoriesResult = _repository.GetAll();
            Categories = CategoriesResult.Value?.ToList() ?? new List<Category>();
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);

    }
}