using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
