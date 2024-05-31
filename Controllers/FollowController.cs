using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyModa.Models;

namespace TrendyModa.Controllers
{
    public class FollowController : Controller
    {
        public readonly TrendyModaDbContext context;
        public FollowController()
        {
            context = new TrendyModaDbContext();
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
