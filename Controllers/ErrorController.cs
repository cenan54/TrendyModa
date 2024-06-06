using Microsoft.AspNetCore.Mvc;

namespace TrendyModa.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
