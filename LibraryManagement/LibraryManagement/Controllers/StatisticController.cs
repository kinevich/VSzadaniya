using LibraryManagement.Data;
using LibraryManagement.Models.ViewModels;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class StatisticController : Controller
    {
        private readonly LibraryManagementContext _db;

        private readonly IStatisticsService _statisticsService;

        public StatisticController(LibraryManagementContext db, IStatisticsService statisticsService)
        {
            _db = db;
            _statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult LibraryAmountDistrict()
        {
            var libraryAmountDistrict = _statisticsService.GetLibraryAmountDistrict();

            return View(libraryAmountDistrict);
        }

        public IActionResult AuthorAmountGenre()
        {
            var authorAmountGenre = _statisticsService.GetAuthorAmountGenre();

            return View(authorAmountGenre);
        }

        public IActionResult TopAuthorLibrary()
        {
            var authorAmountGenre = _statisticsService.GetTopAuthorLibrary();

            return View(authorAmountGenre);
        }
    }
}
