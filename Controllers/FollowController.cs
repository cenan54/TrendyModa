﻿using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TrendyModa.Models;
using TrendyModa.ViewModels;
using Microsoft.EntityFrameworkCore;
using TrendyModa.Repositories;

namespace TrendyModa.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        public readonly TrendyModaDbContext context;
        private readonly FollowRepository followRepository;
        public FollowController()
        {
            context = new TrendyModaDbContext();
            followRepository = new FollowRepository(context);
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

        
            public IActionResult Followers(int id)
            {
                int loggedId = id;
                var followers = followRepository.GetFollowers(loggedId);
                return View(followers);
            }

            public IActionResult Followings(int id)
            {
                int loggedId = id;
                var followings = followRepository.GetFollowings(loggedId);
                return View(followings);
            }
        



    }
}
