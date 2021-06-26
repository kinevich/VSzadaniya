using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using WebImageConvolution.Models;
using WebImageConvolution.Services;

namespace WebImageConvolution.Controllers
{
    public class ConvolutionController : Controller
    {
        private readonly IConvolutionService _convolutionService;

        public ConvolutionController(IConvolutionService convolutionService)
        {
            _convolutionService = convolutionService;
        }

        public IActionResult Index(string path)
        {
            var imageModel = new ImageModel
            {
                Name = Path.GetFileName(path),
                Path = path
            };
            return View(imageModel);
        }
         
        public IActionResult Blur(string path) 
        {
            var bitmap = new Bitmap(path);
            bitmap = _convolutionService.Blur(bitmap);
            SaveImage(bitmap, path);

            return RedirectToAction("Index", path);
        }

        public IActionResult Download(string path)
        {
            return PhysicalFile(path, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }

        private void SaveImage(Bitmap bitmap, string path) 
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            bitmap.Save(path);
        }
    }
}
