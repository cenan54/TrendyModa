using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyModa.Models;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly TrendyModaDbContext context;

        public UserProfileController()
        {
            context = new TrendyModaDbContext();
        }


        [HttpGet]
        public IActionResult MyAccount()
        {
            int claimId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var u = context.Users.FirstOrDefault(y => y.UserId == claimId);
            return View(u);
        }


        [HttpPost]
        public IActionResult MyAccountEdit([FromForm] User data)
        {
            if (ModelState.IsValid)
            {
                context.Users.Update(data);
                context.SaveChanges();
                return RedirectToAction("LogoutIndex","Account");
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
