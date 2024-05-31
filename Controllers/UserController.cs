using Microsoft.AspNetCore.Mvc;
using TrendyModa.Models;

namespace TrendyModa.Controllers
{
    public class UserController : Controller
    {
        private readonly TrendyModaDbContext context;
        public UserController()
        {
            context = new TrendyModaDbContext();
        }
        public IActionResult ListUsers()
        {
            var u = context.Users.ToList();
            return View(u);
        }

    }
}
