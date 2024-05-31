using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TrendyModa.Models;
using TrendyModa.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        public readonly TrendyModaDbContext context;
        public FollowController()
        {
            context = new TrendyModaDbContext();
        }

        [HttpPost]
        public IActionResult FollowAdd([FromForm] FollowAddVM _fallowee)
        {
            int follower = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            int followee = _fallowee.FalloweeId;

            
            Follow f = new Follow
            {
                FalloweeId = followee,
                FallowerId = follower
            };
            context.Follows.Add(f);
            context.SaveChanges();
            


            return RedirectToAction("Index","Home");
        }
    }
}
