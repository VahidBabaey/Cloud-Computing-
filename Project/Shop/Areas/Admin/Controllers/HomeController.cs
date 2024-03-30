using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [Area("Admin")]
    [Authorize]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment hosting;
        public HomeController(IHostingEnvironment _hosting)
        {
            hosting = _hosting;
        }
        public IActionResult Index()
        {
          
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        
        public async Task<IActionResult> SaveFileAsync(IFormFile files)
        {
            var req = Request;
            if (!files.ContentType.Contains("image"))
                return new StatusCodeResult(403);
            string name = Guid.NewGuid().ToString();
            var path = Path.Combine(hosting.WebRootPath, "Pictures/" + name+".jpg");

            if(files.Length>0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                   await files.CopyToAsync(stream);
                }
            }

            return Ok();
        }
        [HttpPost]
        public IActionResult Save(person person)
        {

            return View();
        }

    }
    public class person
    {
        public DateTime BirthDate { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}

