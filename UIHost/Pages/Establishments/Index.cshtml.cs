using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UIHost.Security;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using EstablishmentService.Domain.Models;


namespace UIHost.Pages.Establishments
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Establishment> _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<Establishment> Establishments { get; set; } = new();
        public Result<IEnumerable<Establishment>> ResultEstablishmentsGetAll { get; set; } 

        public IndexModel(IRepositoryServiceFactory<Establishment> factory, IdProtector idProtector)
        {
            _repository = factory.CreateRepositoryService();
            _idProtector =  idProtector;
        }

        public void OnGet()
        {
            ResultEstablishmentsGetAll = _repository.GetAll();
        }

        public void OnPost()
        {
            ResultEstablishmentsGetAll = string.IsNullOrWhiteSpace(SearchTerm) ? _repository.GetAll() : _repository.Search(SearchTerm);
        }

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}