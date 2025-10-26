using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Common.Infrastructure.Persistence.Database;
using TariffingService.Infrastructure.Adapters;
using Common.Domain.SharedPorts;
using TariffingService.Domain.Models;

namespace UIHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISharedDbRepository<Category> _categoryRepository;
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Category> Categories;

        public IndexModel(ILogger<IndexModel> logger, ISharedDbRepository<Category> sharedDbRepository)
        {
            _logger = logger;
            _categoryRepository = sharedDbRepository;
        }

        public void OnGet()
        {
            Categories = _categoryRepository.GetAll();
        }
    }
}
