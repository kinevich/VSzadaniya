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
    }
}
