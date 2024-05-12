using Microsoft.AspNetCore.Mvc;

namespace TrendyModa.Controllers
{
    public class PostController : Controller
    {
        public IActionResult PostAdd()
        {
            return View();
        }
    }
}
