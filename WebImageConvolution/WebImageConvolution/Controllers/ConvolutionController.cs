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
using WebImageConvolution.Helpers;
using WebImageConvolution.Models;
using WebImageConvolution.Services;

namespace WebImageConvolution.Controllers
{
    public class ConvolutionController : Controller
    {
        private readonly IConvolutionService _convolutionService;
        private readonly IImageDataService _imageDataService;

        public ConvolutionController(IConvolutionService convolutionService, IImageDataService imageDataService)
        {
            _convolutionService = convolutionService;
            _imageDataService = imageDataService;
        }

        public IActionResult Index()
        {
            var base64Image = ImageConvertHelper.ImageToBase64String(_convolutionService.Image);
            string imageDataURL = string.Format("data:image/png;base64,{0}", base64Image);

            ViewData["ConvImage"] = imageDataURL;
            return View();
        }

        public IActionResult Blur() 
        {
            _convolutionService.Blur();
            return RedirectToAction("Index");
        }

        public IActionResult Contrast()
        {
            _convolutionService.Contrast();
            return RedirectToAction("Index");
        }

        public IActionResult GrayScale()
        {
            _convolutionService.GrayScale();
            return RedirectToAction("Index");
        }

        public IActionResult Edges()
        {
            _convolutionService.Edges();
            return RedirectToAction("Index");
        }

        public IActionResult Flip()
        {
            _convolutionService.Flip();
            return RedirectToAction("Index");
        }

        public IActionResult ImpulseNoise()
        {
            _convolutionService.ImpulseNoise();
            return RedirectToAction("Index");
        }

        public IActionResult Log()
        {
            _convolutionService.Log();
            return RedirectToAction("Index");
        }

        public IActionResult Negate()
        {
            _convolutionService.Negate();
            return RedirectToAction("Index");
        }

        public IActionResult SobelEdges()
        {
            _convolutionService.SobelEdges();
            return RedirectToAction("Index");
        }

        public IActionResult Reset()
        {
            _convolutionService.Image = _imageDataService.OriginalImage;
            return RedirectToAction("Index");
        }

        public IActionResult Download()
        {
            var imageBytes = ImageConvertHelper.ImageToBytes(_convolutionService.Image);

            return File(imageBytes, MediaTypeNames.Application.Octet, _imageDataService.Name);
        }
    }
}
