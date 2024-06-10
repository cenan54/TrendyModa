using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet]
        public IActionResult MyAccountEditIndex()
        {
            int claimId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var u = context.Users.FirstOrDefault(y => y.UserId == claimId);
            return View(u);
        }


        [HttpPost]
        public IActionResult MyAccountEdit([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<User>();
                var hashedPassword = passwordHasher.HashPassword(user, user.Password);
                user.Password = hashedPassword;
                context.Users.Update(user);
                context.SaveChanges();
                return RedirectToAction("LogoutIndex","Account");
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
