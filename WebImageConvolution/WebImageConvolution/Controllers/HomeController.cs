using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebImageConvolution.Models;

namespace WebImageConvolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _dir;
        private readonly string[] _permittedExtensions = { ".png", ".jpg", "jpeg" };

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
            _dir = _env.ContentRootPath;
        }

        public IActionResult Index() => View();

        public IActionResult SingleFile(IFormFile image)
        {
            var ext = Path.GetExtension(image.FileName);

            if (!_permittedExtensions.Contains(ext)) 
            {
                return RedirectToAction("Index");
            }

            var filePath = Path.Combine(_dir, image.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                image.CopyTo(fileStream);
            }

            return RedirectToAction("Index", "Convolution", filePath);
        }
    }
}
