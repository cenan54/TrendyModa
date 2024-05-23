﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TrendyModa.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;



namespace TrendyModa.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly TrendyModaDbContext context;
        public PostController()
        {
            context = new TrendyModaDbContext();
        }

        [HttpGet]
        public IActionResult PostAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostAddAction(IFormFile Image, string description)
        {
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    IFormFile imageFile = Image;
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // Yeni bir photo nesnesi veritabanına kayıt
                    var photo = new Photo
                    {
                        UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value),
                        Description = description,
                        Image = imageBytes,
                        CreatedAt = DateTime.Now
                      
                    };
                    context.Photos.Add(photo);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index","Home"); // başka bir view'a yönlendirme 
        }

        [HttpGet]
        public IActionResult GetImage()
        {
            var images = context.Photos.ToList();
            return View(images);
        }

        //[HttpGet]
        //public IActionResult GetImage(int id)
        //{
        //    var photoModel = context.Photos.Find(id);
        //    if (photoModel != null)
        //    {
        //        return File(photoModel.Image, "image/jpeg");
        //    }
        //    return NotFound();
        //}

        //[HttpGet]
        //public IActionResult GetImage()
        //{
        //    List<Photo> photos = context.Photos.ToList();
        //    var imageModels = photos.Select(p => new
        //    {
        //        p.Description,
        //        ImageData = $"data:image/jpeg;base64,{Convert.ToBase64String(p.Image)}"
        //    }).ToList();


        //    return View(imageModels);
        //}


    }

   
}
