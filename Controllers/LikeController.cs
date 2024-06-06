using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyModa.Models;
using TrendyModa.ViewModels;


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
        public IActionResult LikeAdd([FromForm] Like l)
        {
            if (l != null)
            {

              //  var existingLike = context.Likes.FirstOrDefault(l => l.UserId == userId && l.PhotoId == photoId);
                context.Likes.Add(l);
                context.SaveChanges();
                return RedirectToAction("GetImage", "Post");
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}
