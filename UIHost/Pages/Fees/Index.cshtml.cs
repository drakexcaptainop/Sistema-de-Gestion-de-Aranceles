using Microsoft.AspNetCore.Mvc.RazorPages;

using UIHost.Security;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using Microsoft.AspNetCore.Mvc;
using TariffingService.Domain.Models;

namespace UIHost.Pages.Fees
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Fee> _repository;
        private readonly IRepositoryService<Category> _categoryRepository;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public Result<IEnumerable<Fee>> ResultFeeGetAll { get; set; }

        public List<Fee> Fees { get; set; } = new();

        public IndexModel(IRepositoryServiceFactory<Fee> factory, IRepositoryServiceFactory<Category> categoryFactory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _categoryRepository = categoryFactory.CreateRepositoryService();
        }

        public void OnGet()
        {
            ResultFeeGetAll = _repository.GetAll();
            if(ResultFeeGetAll.IsFailure) return;
            var categoryList = _categoryRepository.GetAll();

            foreach (var fee in ResultFeeGetAll.Value)
            {
                fee.Category = categoryList.Value.FirstOrDefault(c => c.Id == fee.CategoryId);
            }
        }

        public void OnPostSearch()
        {
            ResultFeeGetAll = string.IsNullOrWhiteSpace(SearchTerm) ? _repository.GetAll() : _repository.Search(SearchTerm);
            Fees = ResultFeeGetAll.Value?.ToList() ?? new List<Fee>();
        }
    }
}