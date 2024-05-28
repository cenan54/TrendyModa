using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrendyModa.Models;

namespace TrendyModa.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly TrendyModaDbContext context;
        public AccountController()
        {
            context = new TrendyModaDbContext();
        }

        //Signup sayfa get action'i
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        //Signup action'i
        [HttpPost]
        public IActionResult Signup([FromForm] User data)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(data);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
          return View(data);
        }

        //Sayfa get action'i
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Login islemi
        [HttpPost]
        public async Task<IActionResult> Login([FromForm, Bind("Email", "Password")] User dataUser)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == dataUser.Email && x.Password == dataUser.Password);

			if (user == null)
			{
				return View(dataUser);
			}

			var claims = new List<Claim> {
				new Claim(ClaimTypes.Email,user.Email),
				new Claim("UserPassword",user.Password),
				new Claim("Username", user.Username),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Surname,user.Lastname),
                new Claim("UserId",user.UserId.ToString()),

			};


			var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties(); ;
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

			return RedirectToAction("Index", "Home");
		}

        // Logout islemi
        public async Task<IActionResult> LogoutIndex()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

      
    }
}
