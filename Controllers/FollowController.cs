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
        public IActionResult FollowAddOrRemove([FromForm] Follow flw)
        {
           if (flw == null)
            {
                return RedirectToAction("Index", "Error");
            }
            else
            {
                var f = context.Follows.FirstOrDefault(x=>x.FallowerId==flw.FallowerId && x.FalloweeId==flw.FalloweeId);

                if (f==null)
                {
                    context.Follows.Add(flw);
                    context.SaveChanges();
                    return RedirectToAction("UsersList", "User");
                }
                else
                {
                    context.Follows.Remove(f);
                    context.SaveChanges();
                    return RedirectToAction("UsersList", "User");
                }

                
            }
            
        }
    }
}
