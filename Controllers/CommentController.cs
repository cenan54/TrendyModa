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

        [HttpGet]
        public IActionResult AddEditCommentIndex(int id)
        {
            var comment = context.Comments.FirstOrDefault(x => x.PhotoId==id);
            if (comment == null)
            {
                return View("AddComment",id);
            }
            else
            {
                return View("EditComment",comment);
            }
            
            
        }

        [HttpPost]
        public IActionResult AddComment([FromForm] Comment comment)
        {
           if (comment == null)
            {
                return RedirectToAction("Index", "Error");
            }
           else
            {
                context.Comments.Add(comment);
                context.SaveChanges();
                return RedirectToAction("GetImage", "Post");
            }
           
        }

        [HttpPost]
        public IActionResult EditComment([FromForm] Comment comment)
        {
            if (comment!=null)
            {
                //context.Comments.Update(comment);
                //context.SaveChanges();
                //return RedirectToAction("GetImage", "Post");

                Comment c = context.Comments.FirstOrDefault(x => x.PhotoId == comment.PhotoId && x.UserId == comment.UserId);
                c.CommentText = comment.CommentText;
                context.Comments.Update(c);
                context.SaveChanges();
                return RedirectToAction("GetImage", "Post");
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            return View();
        }
    }
}
