using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TrendyModa.Models;
using Microsoft.EntityFrameworkCore;
using TrendyModa.ViewModels;




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
            if (Image != null && Image.Length > 0 && ModelState.IsValid)
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
            var images = context.Photos.Include(p => p.User).Include(l => l.Likes).ToList();
            
            
                return View(images);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var p = context.Photos.FirstOrDefault(x => x.PhotoId == Id);
            return View(p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var p = context.Photos.FirstOrDefault(x => x.PhotoId == id);
            context.Photos.Remove(p);
            var count = context.SaveChanges();
            if (count > 0)
            {
                return RedirectToAction("GetImage","Post");
            }
            else
            {
                return View("PostDelete", p);
            }
        }

        [HttpGet]
        public IActionResult PostEdit(int id)
        {
            var p = context.Photos.FirstOrDefault(x => x.PhotoId==id);
            var e = new PostEditVM { 
                PhotoId = p.PhotoId,
                Description = p.Description
            };
            return View(e);
        }


        [HttpPost]
        public IActionResult PostEditConfirmed(PostEditVM model)
        {
            int postId = model.PhotoId;
            string description = model.Description;

            // Veritabanında güncelleme işlemini gerçekleştirin
            // Örneğin, Entity Framework ile:
            var existingRecord = context.Photos.FirstOrDefault(r => r.PhotoId == postId);
            if (existingRecord != null)
            {
                existingRecord.Description = description;
                context.SaveChanges();
              //  return RedirectToAction("Details", new { id = existingRecord.PhotoId });
                return RedirectToAction("GetImage","Post");
            }
            else
            {
                // Hata durumunda kullanıcıyı hata sayfasına yönlendirin
                return RedirectToAction("Error");
            }
        }
    }

    }

   


