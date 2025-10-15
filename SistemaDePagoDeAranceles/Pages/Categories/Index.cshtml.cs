using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Factory;

namespace SistemaDePagoDeAranceles.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<Category> _repository;

        public List<Category> Categories { get; set; } = new();

        public IndexModel(IRepositoryServiceFactory<Category> factory)
        {
            _repository = factory.CreateRepositoryService();
        }

        public void OnGet()
        {
            Categories = _repository.GetAll().Where(category => category.Active).ToList();
        }
    }
}