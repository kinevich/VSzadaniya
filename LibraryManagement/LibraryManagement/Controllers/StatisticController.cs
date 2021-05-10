using LibraryManagement.Data;
using LibraryManagement.Models.ViewModels;
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

        public StatisticController(LibraryManagementContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LibraryAmountDistrict()
        {
            var libraryAmountDistrict = from district in _db.District.AsEnumerable()
                                        join library in _db.Library.AsEnumerable() on district.Id equals library.DistrictId
                                        group library by district into g
                                        select new LibraryAmountDistrictVM { District = g.Key, LibraryAmount = g.Count() };

            return View(libraryAmountDistrict);
        }

        public IActionResult AuthorAmountGenre()
        {
            var authorAmountGenre = from author in _db.Author.AsEnumerable()
                                    join book in _db.Book.AsEnumerable() on author.Id equals book.AuthorId
                                    join genre in _db.Genre.AsEnumerable() on book.GenreId equals genre.Id
                                    group author by genre into g
                                    select new AuthorAmountGenreVM 
                                    {
                                        Genre = g.Key,
                                        AuthorAmount = (from author in g
                                                       group author by author into g1
                                                       select g1.Key).Count()
                                    };

            return View(authorAmountGenre);
        }

        public IActionResult TopAuthorLibrary()
        {
            var authorAmountGenre = from author in _db.Author.AsEnumerable()
                                    join book in _db.Book.AsEnumerable() on author.Id equals book.AuthorId
                                    join bookLibrary in _db.BookLibrary.AsEnumerable() on book.Id equals bookLibrary.BookId
                                    join library in _db.Library.AsEnumerable() on bookLibrary.LibraryId equals library.Id
                                    group author by library into g
                                    select new TopAuthorLibraryVM
                                    {
                                        Library = g.Key,
                                        TopAuthor = (from author in g
                                                    group author by author into g1
                                                    select new 
                                                    {Author = g1.Key, Count = g1.Count()}).OrderByDescending(i =>i.Count).Select(i => i.Author).FirstOrDefault()
                                    };

            return View(authorAmountGenre);
        }
    }
}
