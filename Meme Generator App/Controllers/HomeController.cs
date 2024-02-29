using Meme_Generator_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Meme_Generator_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private string pathImage = "https://localhost:7007/files/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile upload)
        {
            string fileName;
            if (upload != null && upload.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }
               
                return View("Index", pathImage + fileName);
            }
            ViewBag.Error = "File not loaded";
            return View("Index", null);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
