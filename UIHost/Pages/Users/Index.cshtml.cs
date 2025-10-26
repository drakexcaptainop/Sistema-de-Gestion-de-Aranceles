using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagementService.Application.RepositoryServices;
using UserManagementService.Domain.Models;
using UIHost.Security;
using Common.Domain.Patterns;

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

        public IndexModel(IUserRepositoryService userService, IdProtector idProtector)
        {
            _repository = userService;
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
