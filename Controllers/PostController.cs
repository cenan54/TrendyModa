using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> PostAddAction(IFormFile imageFile, string description)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // Yeni bir User nesnesi oluşturun ve veritabanına kaydedin.
                    var photo = new Photo
                    {
                        UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value),
                        Description = description,
                        Image = imageBytes
                        // Diğer sütunlar için de benzer şekilde...
                    };
                    context.Photos.Add(photo);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index","Home"); // veya başka bir view'a yönlendirme yapın.
        }






    }

}

