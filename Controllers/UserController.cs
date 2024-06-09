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

        [HttpGet]
        public IActionResult UsersList()
        {
            var users = context.Users.ToList();
            return View(users);
        }

    }
}
