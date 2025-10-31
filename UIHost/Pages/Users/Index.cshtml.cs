using System.Collections.Generic;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UIHost.Security;
using UserManagementService.Application.RepositoryServices;
using UserManagementService.Domain.Models;

namespace UIHost.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepositoryService _repository;
        private readonly IdProtector _idProtector;

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<User> Users { get; set; } = new();
        public Result<IEnumerable<User>> ResultGetAllUser { get; set; }

        public IndexModel(IRepositoryServiceFactory<User> userService, IdProtector idProtector)
        {
            _repository = (IUserRepositoryService)userService.CreateRepositoryService();
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
