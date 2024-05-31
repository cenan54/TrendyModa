using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyModa.Models;
using TrendyModa.ViewModels;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly TrendyModaDbContext context;
        public UserController()
        {
            context = new TrendyModaDbContext();
        }
        public IActionResult UsersList()
        {
            UserFollowListVM vm = new UserFollowListVM();
            vm.Users = context.Users.ToList();
            vm.Follows = context.Follows.ToList();
            return View(vm);
        }

    }
}
