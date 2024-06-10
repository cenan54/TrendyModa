using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrendyModa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TrendyModa.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly TrendyModaDbContext context;
        public AccountController()
        {
            context = new TrendyModaDbContext();
        }

       
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

     

        [HttpPost]
        public IActionResult Signup([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<User>();
                var hashedPassword = passwordHasher.HashPassword(user, user.Password); 
                user.Password = hashedPassword;
                context.Users.Add(user); 
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm, Bind("Email", "Password")] User dataUser)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == dataUser.Email);

            if (user == null || !VerifyHashedPassword(user.Password, dataUser.Password))
            {
                return View(dataUser);
            }

                    var claims = new List<Claim> 
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserPassword", user.Password),
                        new Claim("Username", user.Username),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.Lastname),
                        new Claim("UserId", user.UserId.ToString()),
                    };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        private bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }


        [Authorize]
        public async Task<IActionResult> LogoutIndex()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

      
    }
}
