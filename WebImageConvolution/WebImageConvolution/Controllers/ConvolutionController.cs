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

        public IActionResult Index()
        {
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
            ConvolutionService.Image = ImageDataService.OriginalImage;
            return RedirectToAction("Index");
        }

        public IActionResult Download()
        {
            var converter = new ImageConverter();
            var data = (byte[])converter.ConvertTo(ConvolutionService.Image, typeof(byte[]));

            return File(data, MediaTypeNames.Application.Octet, ImageDataService.Name);
        }
    }
}
