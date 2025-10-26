using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Common.Infrastructure.Persistence.Database;
using TariffingService.Infrastructure.Adapters;
using Common.Domain.SharedPorts;
using TariffingService.Domain.Models;
using TariffingService.Domain.RepositoryPorts;
using EstablishmentService.Domain.Models;
using EstablishmentService.Domain.RepositoryPorts;

namespace UIHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEstablishmentRepository _establismentRespository;
        public IEnumerable<Category> Categories;
        public IEnumerable<Establishment> Establishments;

        public IndexModel(ISharedDbRepository<Establishment> establishmentRepo, ISharedDbRepository<Category> categoryRepo)
        {
            _categoryRepository = (ICategoryRepository)categoryRepo;
            _establismentRespository = (IEstablishmentRepository)establishmentRepo;
        }

        public void OnGet()
        {
            Categories = _categoryRepository.GetAll();
            Establishments = _establismentRespository.GetAll();
        }
    }
}
