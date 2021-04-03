using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class AppointmentController : Controller
    {


        public IActionResult Index()
        {
            return View();
            //var nowDate = DateTime.Now.ToShortDateString();
            //return Ok(nowDate);
        }
    }
}
