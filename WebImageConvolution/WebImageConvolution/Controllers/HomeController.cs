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
        private readonly IImageDataService _imageDataService;
        private readonly IConvolutionService _convolutionService;

        public HomeController(IImageDataService imageDataService, IConvolutionService convolutionService) 
        {
            _imageDataService = imageDataService;
            _convolutionService = convolutionService;
        }

        public IActionResult Index() => View();

        public IActionResult SingleFile(IFormFile image)
        {
            if (image == null) 
            {
                return RedirectToAction("Index");
            }

            var ext = Path.GetExtension(image.FileName);

            if (!_permittedExtensions.Contains(ext)) 
            {
                return RedirectToAction("Index");
            }

            var stream = new MemoryStream();
            image.CopyTo(stream);
            var bitmap = new Bitmap(stream);

            _convolutionService.Image = bitmap;
            _imageDataService.Name = image.FileName;
            _imageDataService.OriginalImage = bitmap;

            return RedirectToAction("Index", "Convolution");
        }
    }
}
