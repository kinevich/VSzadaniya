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
using WebImageConvolution.Services;

namespace WebImageConvolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly string[] _permittedExtensions = { ".png", ".jpg", ".jpeg" };

        public HomeController() { }

        public IActionResult Index() => View();

        public IActionResult SingleFile(IFormFile image)
        {
            var ext = Path.GetExtension(image.FileName);

            if (!_permittedExtensions.Contains(ext)) 
            {
                return RedirectToAction("Index");
            }

            var stream = new MemoryStream();
            image.CopyTo(stream);
            var bitmap = new Bitmap(stream);

            ConvolutionService.Image = bitmap;
            ImageDataService.Name = image.FileName;
            ImageDataService.OriginalImage = bitmap;

            return RedirectToAction("Index", "Convolution");
        }
    }
}
