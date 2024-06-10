using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrendyModa.Models;
using Microsoft.EntityFrameworkCore;
using TrendyModa.Repositories;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;


namespace TrendyModa.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly TrendyModaDbContext context;
        private readonly HomeRepository homeRepository;
        

        public HomeController()
        {
            context = new TrendyModaDbContext();
            homeRepository = new HomeRepository(context);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int userId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var posts = await homeRepository.GetFollowedUsersPostsAsync(userId);
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
