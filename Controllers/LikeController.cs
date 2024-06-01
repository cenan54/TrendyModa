using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyModa.Models;
using TrendyModa.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly TrendyModaDbContext context;

        public LikeController()
        {
            context = new TrendyModaDbContext();
        }

        
        [HttpGet]
        public IActionResult LikeList(int id)
        {
            var likes = context.Likes
                .Where(x => x.PhotoId == id)
                .Include(l => l.User)
                .ToList();

            return View(likes);
        }

        [HttpPost]
        public IActionResult LikeAdd(int data)
        {
            Photo p = context.Photos.FirstOrDefault(x => x.PhotoId == data);
            int u = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            if (p!=null && u!=null)
            {
                var l = new Like
                {
                    UserId = u,
                    PhotoId = p.PhotoId
                };
                context.Add(l);
                context.SaveChanges();
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
