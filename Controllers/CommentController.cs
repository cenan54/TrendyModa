using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyModa.Models;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly TrendyModaDbContext context;

        public CommentController()
        {
            context = new TrendyModaDbContext();
        }

        [HttpGet]
        public IActionResult ListComments(int id)
        {
           // var c = context.Likes.FirstOrDefault(x=>x.PhotoId==photoId);
            var comments = context.Comments.Where(x=>x.PhotoId==id).Include(u => u.User).ToList();
            return View(comments);
        }
    }
}
