using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            (List<VisitorLibrary> visitorsLibraries, List<Visitor> visitors, List<Library> libraries,
             List<District> districts, City city, List<Book> books, List<Genre> genres, List<Edition> editions,
             List<BookCopy> booksCopies, List<BookCopyLibrary> booksCopiesLibraries, List<Visit> visits) = GetData();

            //ListLibraryNames(libraries);
            //ListGroupedLibrariesByDistricts(libraries, districts);
            //ListDistrictsDontHaveLibraries(libraries, districts);
            //ListAllVisitors(libraries, visitors);
            //ListVisitorsByLibraries(libraries, visitors, visitorsLibraries);
            //ListVisitorsByDistricts(libraries, districts, visitors, visitorsLibraries);
            //ListVisitorsCountByDistricts(libraries, districts, visitorsLibraries);
            //ListBooksCountByGenres(books, genres);
            //ListBooksCountByGenresByLibraries(books, genres, libraries, booksCopiesLibraries, booksCopies);
            //ListBooksCopiesByGenres(books, booksCopies, genres);
            //ShowTheMostReadBook(visits, booksCopiesLibraries, booksCopies, books);
            //ListMostReadBooksByLibraries(visits, booksCopiesLibraries, booksCopies, books, libraries);
            //ShowTheMostReadGenre(visits, booksCopiesLibraries, booksCopies, books, genres);
            //ListTheMostReadGenresByLibraries(visits, booksCopiesLibraries, booksCopies, books, genres, libraries);
            //ListTopTwoBooksByGenres(visits, booksCopiesLibraries, booksCopies, books, genres);
            //TopTwoBooksByGenresByLibraries(visits, booksCopiesLibraries, booksCopies, books, libraries, genres);

            ShowTheMostReadBookByMonths(visits, booksCopiesLibraries, booksCopies, books);
            ListMostReadBooksByLibrariesByMonths(visits, booksCopiesLibraries, booksCopies, books, libraries);
            ShowTheMostReadGenreByMonths(visits, booksCopiesLibraries, booksCopies, books, genres);
            ListTheMostReadGenresByLibrariesByMonths(visits, booksCopiesLibraries, booksCopies, books, genres, libraries);
            ListTopTwoBooksByGenresByMonths(visits, booksCopiesLibraries, booksCopies, books, genres);
            TopTwoBooksByGenresByLibrariesByMonths(visits, booksCopiesLibraries, booksCopies, books, libraries, genres);

            ShowTheMostReadBookByYears(visits, booksCopiesLibraries, booksCopies, books);
            ListMostReadBooksByLibrariesByYears(visits, booksCopiesLibraries, booksCopies, books, libraries);
            ShowTheMostReadGenreByYears(visits, booksCopiesLibraries, booksCopies, books, genres);
            ListTheMostReadGenresByLibrariesByYears(visits, booksCopiesLibraries, booksCopies, books, genres, libraries);
            ListTopTwoBooksByGenresByYears(visits, booksCopiesLibraries, booksCopies, books, genres);
            TopTwoBooksByGenresByLibrariesByYears(visits, booksCopiesLibraries, booksCopies, books, libraries, genres);

            ListVisitorsWhoHaveExceededReadingTime(visits, visitorsLibraries, visitors, libraries);
            ListBooksByLibrariesAllCopiesTaken(visits, libraries, booksCopies, booksCopiesLibraries, books); // с этим проблема
            ListTopTwoBooksByReadTimeByGenres(visits, booksCopiesLibraries, booksCopies, books, genres);
            ListTopTwoBooksByReadTimeByGenresByLibraries(visits, booksCopiesLibraries, booksCopies, books, libraries, genres);
        }

        private static void ListLibraryNames(List<Library> libraries)
        {
            var query = from l in libraries
                        select l.Name;

            foreach (var libName in query)
            {
                Console.WriteLine(libName);
            }

            Console.WriteLine();
        }

        private static void ListGroupedLibrariesByDistricts(List<Library> libraries, List<District> districts)
        {
            var query = from district in districts
                        join library in libraries on 
                        district.Id equals library.DistrictId into libGroup
                        select new { DistrictName = district.Name, Libraries = libGroup };

            foreach (var group in query)
            {
                Console.WriteLine($"{group.DistrictName}:");
                foreach (var library in group.Libraries)
                    Console.WriteLine($" {library.Name}");
            }

            Console.WriteLine();
        }

        private static void ListDistrictsDontHaveLibraries(List<Library> libraries, List<District> districts)
        {
            var query = from district in districts
                        join library in libraries on
                        district.Id equals library.DistrictId into libGroup
                        where libGroup.Count() == 0
                        select district.Name;

            foreach (var disName in query)
            {
                Console.WriteLine(disName);
            }

            Console.WriteLine();
        }

        private static void ListAllVisitors(List<Library> libraries, List<Visitor> visitors)                                           
        {
            var query = from visitor in visitors
                        select visitor.Name;                        

            foreach (var visName in query)
            {
                Console.WriteLine(visName);
            }

            Console.WriteLine();
        }

        private static void ListVisitorsByLibraries(List<Library> libraries, List<Visitor> visitors,
                                                       List<VisitorLibrary> visitorsLibraries)
        {
            var visitorsByLibraries = from library in libraries
                        join visitorLibrary in visitorsLibraries on library.Id equals visitorLibrary.LibraryId
                        join visitor in visitors on visitorLibrary.VisitorId equals visitor.Id
                        group visitor by library into g
                        select new
                        {
                            LibraryName = g.Key.Name,
                            Visitors = g.Select(v => v.Name)
                        };


            foreach(var visitorsByLibrary in visitorsByLibraries)
            {
                Console.WriteLine($"{visitorsByLibrary.LibraryName}:");

                foreach (var visitorName in visitorsByLibrary.Visitors)
                {
                    Console.WriteLine($" {visitorName}");
                }
            }

            Console.WriteLine();
        }

        private static void ListVisitorsByDistricts(List<Library> libraries, List<District> districts, 
                                                    List<Visitor> visitors, List<VisitorLibrary> visitorsLibrary)                                                    
        {
            var visitorsByDistricts = from district in districts
                                      join library in libraries on district.Id equals library.DistrictId
                                      join visitorLibrary in visitorsLibrary on library.Id equals visitorLibrary.LibraryId
                                      join visitor in visitors on visitorLibrary.VisitorId equals visitor.Id
                                      group visitor by district into g
                                      orderby g.Count() descending
                                      select new
                                      {
                                          DistrictName = g.Key.Name,
                                          Visitors = g.Select(v => v.Name)
                                      };
                                      
            foreach (var visitorsByDistrict in visitorsByDistricts)
            {
                Console.WriteLine($"{visitorsByDistrict.DistrictName}:");

                foreach (var visitorName in visitorsByDistrict.Visitors)
                {
                    Console.WriteLine($" {visitorName}");
                }
            }

            Console.WriteLine();
        }

        private static void ListVisitorsCountByDistricts(List<Library> libraries, List<District> districts,
                                                         List<VisitorLibrary> visitorsLibrary)
        {
            var visitorsCountByDistricts = from district in districts
                                      join library in libraries on district.Id equals library.DistrictId
                                      join visitorLibrary in visitorsLibrary on library.Id equals visitorLibrary.LibraryId
                                      group visitorLibrary by district into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new
                                      {
                                          DistrictName = g.Key.Name,
                                          Count = count  
                                      };

            foreach (var visitorsCountByDistrict in visitorsCountByDistricts)
            {
                Console.WriteLine($"{visitorsCountByDistrict.DistrictName}: {visitorsCountByDistrict.Count}");
            }

            Console.WriteLine();
        }

        private static void ListBooksCountByGenres(List<Book> books, List<Genre> genres)
        {
            var booksByGenres = from genre in genres
                                join book in books on genre.Id equals book.GenreId into g
                                select new { Genre = genre.Name, Count = g.Count() };

            foreach (var booksByGenre in booksByGenres)
            {
                Console.WriteLine($"{booksByGenre.Genre}: {booksByGenre.Count}");
            }

            Console.WriteLine();
        }

        private static void ListBooksCountByGenresByLibraries(List<Book> books, List<Genre> genres, List<Library> libraries,
                                                              List<BookCopyLibrary> booksCopiesLibraries, List<BookCopy> booksCopies)
        {
            var booksCountByGenresByLibraries = from library in libraries
                                                join bookCopyLibrary in booksCopiesLibraries on library.Id equals bookCopyLibrary.LibraryId
                                                join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                                join book in books on bookCopy.BookId equals book.Id
                                                group book by library into g
                                                select new
                                                {
                                                    LibraryName = g.Key.Name,
                                                    Counts = from book in g
                                                             join genre in genres on book.GenreId equals genre.Id
                                                             group book by genre into g1
                                                             select new
                                                             {
                                                                 GenreName = g1.Key.Name,
                                                                 Count = (from book in g1
                                                                          group book by book.Id).Count()
                                                             }
                                                };

            foreach (var booksCountByGenresByLibrary in booksCountByGenresByLibraries)
            {
                Console.WriteLine(booksCountByGenresByLibrary.LibraryName);

                foreach (var booksCountByGenre in booksCountByGenresByLibrary.Counts)
                    Console.WriteLine($" {booksCountByGenre.GenreName} - {booksCountByGenre.Count}");
            }

            Console.WriteLine();
        }

        private static void ListBooksCopiesByGenres(List<Book> books, List<BookCopy> booksCopies, List<Genre> genres)
        {
            var booksCopiesByGenres = from bookCopy in booksCopies
                                      join book in books on bookCopy.BookId equals book.Id
                                      join genre in genres on book.GenreId equals genre.Id
                                      group bookCopy by genre into g
                                      select new
                                      {
                                          GenreName = g.Key.Name,
                                          Count = g.Count()
                                      };

            foreach (var booksCopiesByGenre in booksCopiesByGenres)
                Console.WriteLine($"{booksCopiesByGenre.GenreName}-{booksCopiesByGenre.Count}");

            Console.WriteLine();
        }

        private static void ShowTheMostReadBook(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                List<BookCopy> booksCopies, List<Book> books)
        {
            var theMostReadBook = (from visit in visits
                                  join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                  join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                  join book in books on bookCopy.BookId equals book.Id
                                  group visit by book into g
                                  select new { Title = g.Key.Title, Count = g.Count() }).OrderByDescending(i => i.Count).FirstOrDefault();

            Console.WriteLine(theMostReadBook.Title);

            Console.WriteLine();
        }

        private static void ListMostReadBooksByLibraries(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                         List<BookCopy> booksCopies, List<Book> books, List<Library> libraries)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                        group book by library into g
                        select new
                        {
                            LibraryName = g.Key.Name,
                            MostReadBook = (from book in g
                                           group book by book.Id into g1
                                           select new
                                           {
                                               Title = books.Find(i => i.Id == g1.Key).Title,
                                               Count = g1.Count()
                                           }).OrderByDescending(i => i.Count).FirstOrDefault()
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.LibraryName);
                Console.WriteLine(" " + item.MostReadBook.Title);
            }

            Console.WriteLine();
        }

        private static void ShowTheMostReadGenre(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                 List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var theMostReadGenre = (from visit in visits
                                    join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                    join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                    join book in books on bookCopy.BookId equals book.Id
                                    join genre in genres on book.GenreId equals genre.Id
                                    group book by genre into g
                                    select new
                                    {
                                        GenreName = g.Key.Name,
                                        Count = g.Count()
                                    }).OrderByDescending(i => i.Count).FirstOrDefault();

            Console.WriteLine(theMostReadGenre.GenreName);

            Console.WriteLine();
        }

        private static void ListTheMostReadGenresByLibraries(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                             List<BookCopy> booksCopies, List<Book> books, List<Genre> genres,
                                                             List<Library> libraries)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join genre in genres on book.GenreId equals genre.Id
                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                        group genre by library into g
                        select new
                        {
                            LibraryName = g.Key.Name,
                            MostReadGenre = (from genre in g
                                             group genre by genre.Id into g1
                                             select new
                                             {
                                                 GenreName = genres.Find(g => g.Id == g1.Key).Name,
                                                 Count = g1.Count()
                                             }).OrderByDescending(i => i.Count).FirstOrDefault()
                        };
            
            foreach (var item in query)
            {
                Console.WriteLine(item.LibraryName);
                Console.WriteLine(" " + item.MostReadGenre.GenreName);
            }

            Console.WriteLine();
        }

        private static void ListTopTwoBooksByGenres(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                    List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join genre in genres on book.GenreId equals genre.Id
                        group book by genre into g
                        select new
                        {
                            GenreName = g.Key.Name,
                            TopTwo = (from book in g
                                      group book by book.Id into g1
                                      select new
                                      {
                                          Title = books.Find(b => b.Id == g1.Key).Title,
                                          Count = g1.Count()
                                      }).OrderByDescending(i => i.Count).Take(2)
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.GenreName);

                foreach (var book in item.TopTwo)
                    Console.WriteLine(" " + book.Title);
            }

            Console.WriteLine();
        }

        private static void TopTwoBooksByGenresByLibraries(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                           List<BookCopy> booksCopies, List<Book> books, List<Library> libraries,
                                                           List<Genre> genres)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                        group book by library into g
                        select new
                        {
                            LibraryName = g.Key.Name,
                            Genres = from book in g
                                     join genre in genres on book.GenreId equals genre.Id
                                     group book by genre into g1
                                     select new
                                     {
                                         GenreName = g1.Key.Name,
                                         TopTwo = (from book in g1
                                                   group book by book.Id into g2
                                                   select new
                                                   {
                                                       Title = books.Find(b => b.Id == g2.Key).Title,
                                                       Count = g2.Count()
                                                   }).OrderByDescending(i => i.Count).Take(2)
                                     }
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.LibraryName);

                foreach (var genre in item.Genres)
                {
                    Console.WriteLine(" " + genre.GenreName);

                    foreach (var book in genre.TopTwo)
                        Console.WriteLine("  " + book.Title);
                }
            }

            Console.WriteLine();
        }

        private static void ShowTheMostReadBookByMonths(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                        List<BookCopy> booksCopies, List<Book> books)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        group book by new { visit.WhenGetBook.Year, visit.WhenGetBook.Month } into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            TheMostReadBook = (from book in g
                                               group book by book.Id into g1
                                               select new
                                               {
                                                   Title = books.Find(i => i.Id == g1.Key).Title,
                                                   Count = g.Count()
                                               }).OrderByDescending(i => i.Count).FirstOrDefault()
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}/{item.Month} - {item.TheMostReadBook.Title}");
            }

            Console.WriteLine();
        }

        private static void ListMostReadBooksByLibrariesByMonths(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                 List<BookCopy> booksCopies, List<Book> books, List<Library> libraries)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        group bookCopyLibrary by new { visit.WhenGetBook.Year, visit.WhenGetBook.Month } into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            Libraries = from bookCopyLibrary in g
                                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                        join book in books on bookCopy.BookId equals book.Id
                                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                                        group book by library into g1
                                        select new
                                        {
                                            LibraryName = g1.Key.Name,
                                            TheMostReadBook = (from book in g1
                                                               group book by book.Id into g2
                                                               select new
                                                               {
                                                                   Title = books.Find(i => i.Id == g2.Key).Title,
                                                                   Count = g2.Count()
                                                               }).OrderByDescending(i => i.Count).FirstOrDefault()
                                        }
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}/{item.Month}");

                foreach (var library in item.Libraries)
                    Console.WriteLine($" {library.LibraryName} - {library.TheMostReadBook.Title}");
            }

            Console.WriteLine();
        }

        private static void ShowTheMostReadGenreByMonths(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                         List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join genre in genres on book.GenreId equals genre.Id
                        group genre by new { visit.WhenGetBook.Year, visit.WhenGetBook.Month } into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            TheMostReadGenre = (from genre in g
                                                group genre by genre.Id into g1
                                                select new
                                                {
                                                    GenreName = genres.Find(g => g.Id == g1.Key).Name,
                                                    Count = g1.Count()
                                                }).OrderByDescending(i => i.Count).FirstOrDefault()
                        };


            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}/{item.Month} - {item.TheMostReadGenre.GenreName}");
            }

            Console.WriteLine();
        }

        private static void ListTheMostReadGenresByLibrariesByMonths(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                     List<BookCopy> booksCopies, List<Book> books, List<Genre> genres,
                                                                     List<Library> libraries)
        {
            var query = from visit in visits
                        group visit by new { visit.WhenGetBook.Year, visit.WhenGetBook.Month } into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            Libraries = from visit in g
                                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                        join book in books on bookCopy.BookId equals book.Id
                                        join genre in genres on book.GenreId equals genre.Id
                                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                                        group genre by library into g1
                                        select new
                                        {
                                            LibraryName = g1.Key.Name,
                                            TheMostReadGenre = (from genre in g1
                                                                group genre by genre.Id into g2
                                                                select new
                                                                {
                                                                    GenreName = genres.Find(g => g.Id == g2.Key).Name,
                                                                    Count = g2.Count()
                                                                }).OrderByDescending(i => i.Count).FirstOrDefault()
                                        }
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}/{item.Month}");

                foreach (var library in item.Libraries)
                    Console.WriteLine($"{library.LibraryName} - {library.TheMostReadGenre.GenreName}");
            }

            Console.WriteLine();
        }

        private static void ListTopTwoBooksByGenresByMonths(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                            List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var query = from visit in visits
                        group visit by new { visit.WhenGetBook.Year, visit.WhenGetBook.Month } into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            Genres = from visit in g
                                     join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                     join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                     join book in books on bookCopy.BookId equals book.Id
                                     join genre in genres on book.GenreId equals genre.Id
                                     group book by genre into g1
                                     select new
                                     {
                                         GenreName = g1.Key.Name,
                                         TopTwo = (from book in g1
                                                   group book by book.Id into g2
                                                   select new
                                                   {
                                                       Title = books.Find(b => b.Id == g2.Key).Title,
                                                       Count = g2.Count()
                                                   }).OrderByDescending(i => i.Count).Take(2)
                                     }

                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}/{item.Month}");

                foreach (var genre in item.Genres)
                {
                    Console.WriteLine($" {genre.GenreName}");

                    foreach (var book in genre.TopTwo)
                        Console.WriteLine($"  {book.Title}");
                }

            }

            Console.WriteLine();
        }

        private static void TopTwoBooksByGenresByLibrariesByMonths(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                   List<BookCopy> booksCopies, List<Book> books, List<Library> libraries,
                                                                   List<Genre> genres)
        {
            var query = from visit in visits
                        group visit by new { visit.WhenGetBook.Year, visit.WhenGetBook.Month } into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            Libraries = from visit in g
                                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                        join book in books on bookCopy.BookId equals book.Id
                                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                                        group book by library into g1
                                        select new
                                        {
                                            LibraryName = g1.Key.Name,
                                            Genres = from book in g1
                                                     join genre in genres on book.GenreId equals genre.Id
                                                     group book by genre into g2
                                                     select new
                                                     {
                                                         GenreName = g2.Key.Name,
                                                         TopTwo = (from book in g2
                                                                   group book by book.Id into g3
                                                                   select new
                                                                   {
                                                                       Title = books.Find(b => b.Id == g3.Key).Title,
                                                                       Count = g3.Count()
                                                                   }).OrderByDescending(i => i.Count).Take(2)
                                                     }
                                        }

                        }; 

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}/{item.Month}");

                foreach (var library in item.Libraries)
                {
                    Console.WriteLine($" {library.LibraryName}");

                    foreach (var genre in library.Genres)
                    {
                        Console.WriteLine($"  {genre.GenreName}");

                        foreach (var book in genre.TopTwo)
                            Console.WriteLine($"   {book.Title}");
                    }
                }
            }

            Console.WriteLine();
        }

        private static void ShowTheMostReadBookByYears(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                        List<BookCopy> booksCopies, List<Book> books)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        group book by visit.WhenGetBook.Year into g
                        orderby g.Key
                        select new
                        {
                            Year = g.Key,
                            TheMostReadBook = (from book in g
                                               group book by book.Id into g1
                                               select new
                                               {
                                                   Title = books.Find(i => i.Id == g1.Key).Title,
                                                   Count = g.Count()
                                               }).OrderByDescending(i => i.Count).FirstOrDefault()
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year} - {item.TheMostReadBook.Title}");
            }

            Console.WriteLine();
        }

        private static void ListMostReadBooksByLibrariesByYears(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                 List<BookCopy> booksCopies, List<Book> books, List<Library> libraries)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        group bookCopyLibrary by visit.WhenGetBook.Year into g
                        orderby g.Key
                        select new
                        {
                            Year = g.Key,
                            Libraries = from bookCopyLibrary in g
                                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                        join book in books on bookCopy.BookId equals book.Id
                                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                                        group book by library into g1
                                        select new
                                        {
                                            LibraryName = g1.Key.Name,
                                            TheMostReadBook = (from book in g1
                                                               group book by book.Id into g2
                                                               select new
                                                               {
                                                                   Title = books.Find(i => i.Id == g2.Key).Title,
                                                                   Count = g2.Count()
                                                               }).OrderByDescending(i => i.Count).FirstOrDefault()
                                        }
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}");

                foreach (var library in item.Libraries)
                    Console.WriteLine($" {library.LibraryName} - {library.TheMostReadBook.Title}");
            }

            Console.WriteLine();
        }

        private static void ShowTheMostReadGenreByYears(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                         List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join genre in genres on book.GenreId equals genre.Id
                        group genre by visit.WhenGetBook.Year into g
                        orderby g.Key
                        select new
                        {
                            Year = g.Key,
                            TheMostReadGenre = (from genre in g
                                                group genre by genre.Id into g1
                                                select new
                                                {
                                                    GenreName = genres.Find(g => g.Id == g1.Key).Name,
                                                    Count = g1.Count()
                                                }).OrderByDescending(i => i.Count).FirstOrDefault()
                        };


            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year} - {item.TheMostReadGenre.GenreName}");
            }

            Console.WriteLine();
        }

        private static void ListTheMostReadGenresByLibrariesByYears(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                     List<BookCopy> booksCopies, List<Book> books, List<Genre> genres,
                                                                     List<Library> libraries)
        {
            var query = from visit in visits
                        group visit by visit.WhenGetBook.Year into g
                        orderby g.Key
                        select new
                        {
                            Year = g.Key,
                            Libraries = from visit in g
                                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                        join book in books on bookCopy.BookId equals book.Id
                                        join genre in genres on book.GenreId equals genre.Id
                                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                                        group genre by library into g1
                                        select new
                                        {
                                            LibraryName = g1.Key.Name,
                                            TheMostReadGenre = (from genre in g1
                                                                group genre by genre.Id into g2
                                                                select new
                                                                {
                                                                    GenreName = genres.Find(g => g.Id == g2.Key).Name,
                                                                    Count = g2.Count()
                                                                }).OrderByDescending(i => i.Count).FirstOrDefault()
                                        }
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}");

                foreach (var library in item.Libraries)
                    Console.WriteLine($" {library.LibraryName} - {library.TheMostReadGenre.GenreName}");
            }

            Console.WriteLine();
        }

        private static void ListTopTwoBooksByGenresByYears(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                            List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var query = from visit in visits
                        group visit by visit.WhenGetBook.Year into g
                        orderby g.Key
                        select new
                        {
                            Year = g.Key,
                            Genres = from visit in g
                                     join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                     join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                     join book in books on bookCopy.BookId equals book.Id
                                     join genre in genres on book.GenreId equals genre.Id
                                     group book by genre into g1
                                     select new
                                     {
                                         GenreName = g1.Key.Name,
                                         TopTwo = (from book in g1
                                                   group book by book.Id into g2
                                                   select new
                                                   {
                                                       Title = books.Find(b => b.Id == g2.Key).Title,
                                                       Count = g2.Count()
                                                   }).OrderByDescending(i => i.Count).Take(2)
                                     }

                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}");

                foreach (var genre in item.Genres)
                {
                    Console.WriteLine($" {genre.GenreName}");

                    foreach (var book in genre.TopTwo)
                        Console.WriteLine($"  {book.Title}");
                }

            }

            Console.WriteLine();
        }

        private static void TopTwoBooksByGenresByLibrariesByYears(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                  List<BookCopy> booksCopies, List<Book> books, List<Library> libraries,
                                                                  List<Genre> genres)
        {
            var query = from visit in visits
                        group visit by visit.WhenGetBook.Year into g
                        orderby g.Key
                        select new
                        {
                            Year = g.Key,
                            Libraries = from visit in g
                                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                        join book in books on bookCopy.BookId equals book.Id
                                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                                        group book by library into g1
                                        select new
                                        {
                                            LibraryName = g1.Key.Name,
                                            Genres = from book in g1
                                                     join genre in genres on book.GenreId equals genre.Id
                                                     group book by genre into g2
                                                     select new
                                                     {
                                                         GenreName = g2.Key.Name,
                                                         TopTwo = (from book in g2
                                                                   group book by book.Id into g3
                                                                   select new
                                                                   {
                                                                       Title = books.Find(b => b.Id == g3.Key).Title,
                                                                       Count = g3.Count()
                                                                   }).OrderByDescending(i => i.Count).Take(2)
                                                     }
                                        }

                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Year}");

                foreach (var library in item.Libraries)
                {
                    Console.WriteLine($" {library.LibraryName}");

                    foreach (var genre in library.Genres)
                    {
                        Console.WriteLine($"  {genre.GenreName}");

                        foreach (var book in genre.TopTwo)
                            Console.WriteLine($"   {book.Title}");
                    }
                }
            }

            Console.WriteLine();
        }

        private static void ListVisitorsWhoHaveExceededReadingTime(List<Visit> visits, List<VisitorLibrary> visitorsLibraries,
                                                                   List<Visitor> visitors, List<Library> libraries)
        {
            var query = from visit in visits
                        join visitorLibrary in visitorsLibraries on visit.VisitorLibraryId equals visitorLibrary.Id
                        join visitor in visitors on visitorLibrary.VisitorId equals visitor.Id
                        join library in libraries on visitorLibrary.LibraryId equals library.Id
                        where visit.HowMuchRead > library.Limit
                        group visitor by visitor into g
                        select g.Key.Name;

            foreach (var visitorName in query)
                Console.WriteLine(visitorName);

            Console.WriteLine();
        }

        private static void ListBooksByLibrariesAllCopiesTaken(List<Visit> visits, List<Library> libraries, List<BookCopy> booksCopies,
                                                               List<BookCopyLibrary> booksCopiesLibraries, List<Book> books)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                        where !visit.DidReturnTheBook()
                        group bookCopyLibrary by library into g
                        select new
                        {
                            LibraryName = g.Key.Name,
                            Books = from bookCopyLibrary in g
                                    join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                    join book in books on bookCopy.BookId equals book.Id
                                    group bookCopy by book into g1
                                    where g1.Count() == booksCopies.Select(b => b.BookId == g1.Key.Id).Count()
                                    select g1.Key.Title
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.LibraryName);

                foreach (var bookTitle in item.Books)
                {
                    Console.WriteLine(bookTitle);
                }
            }
        }

        private static void ListTopTwoBooksByReadTimeByGenres(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                              List<BookCopy> booksCopies, List<Book> books, List<Genre> genres)
        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                        join book in books on bookCopy.BookId equals book.Id
                        join genre in genres on book.GenreId equals genre.Id
                        group visit by genre into g
                        select new
                        {
                            GenreName = g.Key.Name,
                            TopTwoByReadTime = (from visit in g
                                                join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                                join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                                join book in books on bookCopy.BookId equals book.Id
                                                group visit by book into g1
                                                select new
                                                {
                                                    BookTitle = g1.Key.Title,
                                                    ReadTime = (from visit in g1
                                                                select visit.HowMuchRead.Ticks).Sum()
                                                }).OrderByDescending(rt => rt.ReadTime).Take(2)
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.GenreName);

                foreach (var book in item.TopTwoByReadTime)
                    Console.WriteLine(" " + book.BookTitle);
            }
        }

        private static void ListTopTwoBooksByReadTimeByGenresByLibraries(List<Visit> visits, List<BookCopyLibrary> booksCopiesLibraries,
                                                                         List<BookCopy> booksCopies, List<Book> books,
                                                                         List<Library> libraries, List<Genre> genres)

        {
            var query = from visit in visits
                        join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                        join library in libraries on bookCopyLibrary.LibraryId equals library.Id
                        group visit by library into g
                        select new
                        {
                            LibraryName = g.Key.Name,
                            Genres = from visit in g
                                     join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                     join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                     join book in books on bookCopy.BookId equals book.Id
                                     join genre in genres on book.GenreId equals genre.Id
                                     group visit by genre into g1
                                     select new
                                     {
                                         GenreName = g1.Key.Name,
                                         TopTwoByReadTime = (from visit in g1
                                                             join bookCopyLibrary in booksCopiesLibraries on visit.BookCopyLibraryId equals bookCopyLibrary.Id
                                                             join bookCopy in booksCopies on bookCopyLibrary.BookCopyId equals bookCopy.Id
                                                             join book in books on bookCopy.BookId equals book.Id
                                                             group visit by book into g2
                                                             select new
                                                             {
                                                                 BookTitle = g2.Key.Title,
                                                                 ReadTime = (from visit in g2
                                                                             select visit.HowMuchRead.Ticks).Sum()
                                                             }).OrderByDescending(rt => rt.ReadTime).Take(2)
                                     }
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.LibraryName);

                foreach (var genre in item.Genres)
                {
                    Console.WriteLine(" " + genre.GenreName);

                    foreach (var book in genre.TopTwoByReadTime)
                        Console.WriteLine("  " + book.BookTitle);
                }
            }
        }

        private static (List<VisitorLibrary> visitorsLibraries, List<Visitor> visitors,
                        List<Library> libraries, List<District> districts, City city,
                        List<Book> books, List<Genre> genres, List<Edition> editions,
                        List<BookCopy> booksCopies, List<BookCopyLibrary> booksCopiesLibraries,
                        List<Visit> visits) GetData()
        {
            var Chester = new City("Chester");

            var districts = new List<District>();

            var WacitGrove = new District("WacitGrove", Chester.Id);
            districts.Add(WacitGrove);
            var ButalpNorth = new District("ButalpNort", Chester.Id);
            districts.Add(ButalpNorth);
            var CherriftWood = new District("CherriftWood", Chester.Id);
            districts.Add(CherriftWood);
            var BloodPlaza = new District("Blood Plaza", Chester.Id);
            districts.Add(BloodPlaza);
            var NorthLownerd = new District("North Lownerd", Chester.Id);
            districts.Add(NorthLownerd);

            var libraries = new List<Library>();

            var TempleLibrary = new Library("Temple Library", BloodPlaza.Id, new TimeSpan(10, 0, 0, 0)); 
            libraries.Add(TempleLibrary);
            var ObeliskLibrary = new Library("Obelisk Library", BloodPlaza.Id, new TimeSpan(15, 0, 0, 0)); 
            libraries.Add(ObeliskLibrary);
            var AlgorithmLibrary = new Library("Algorithm Library", BloodPlaza.Id, new TimeSpan(16, 0, 0, 0)); 
            libraries.Add(AlgorithmLibrary);
            var DaydreamLibrary = new Library("Daydream Library", NorthLownerd.Id, new TimeSpan(20, 0, 0, 0));
            libraries.Add(DaydreamLibrary);
            var AmenityLibrary = new Library("Amenity Library", NorthLownerd.Id, new TimeSpan(12, 0, 0, 0)); 
            libraries.Add(AmenityLibrary);
            var BeverlyLibrary = new Library("Beverly Library", ButalpNorth.Id, new TimeSpan(17, 0, 0, 0)); 
            libraries.Add(BeverlyLibrary);
            var AeosLibrary = new Library("Aeos Library", CherriftWood.Id, new TimeSpan(21, 0, 0, 0)); 
            libraries.Add(AeosLibrary);            

            var visitors = new List<Visitor>();

            var SanjeevAdams = new Visitor("Sanjeev Adams"); 
            visitors.Add(SanjeevAdams);
            var ClaireMackie = new Visitor("Claire Mackie"); 
            visitors.Add(ClaireMackie);
            var HaleyBarnard = new Visitor("Haley Barnard");
            visitors.Add(HaleyBarnard);
            var LeightonAndersen = new Visitor("Leighton Andersen"); 
            visitors.Add(LeightonAndersen);
            var MattFitzgerald = new Visitor("Matt Fitzgerald"); 
            visitors.Add(MattFitzgerald);
            var GinoPearce = new Visitor("Gino Pearce"); 
            visitors.Add(GinoPearce);
            var RehaanYork = new Visitor("Rehaan York");
            visitors.Add(RehaanYork);
            var VerityMorton = new Visitor("Verity Morton"); 
            visitors.Add(VerityMorton);
            var KadeTravis = new Visitor("Kade Travis"); 
            visitors.Add(KadeTravis);
            var KhalidHarding = new Visitor("Khalid Harding"); 
            visitors.Add(KhalidHarding);
            var HasnainKearney = new Visitor("Hasnain Kearney");
            visitors.Add(HasnainKearney);
            var BeaudenNielsen = new Visitor("Beauden Nielsen");
            visitors.Add(BeaudenNielsen);
            var KellanConroy = new Visitor("Kellan Conroy"); 
            visitors.Add(KellanConroy);

            var visitorsLibraries = new List<VisitorLibrary>();

            var SanjeevAdams_TempleLibrary = new VisitorLibrary(SanjeevAdams.Id, TempleLibrary.Id);
            visitorsLibraries.Add(SanjeevAdams_TempleLibrary);
            var SanjeevAdams_DaydreamLibrary = new VisitorLibrary(SanjeevAdams.Id, DaydreamLibrary.Id);
            visitorsLibraries.Add(SanjeevAdams_DaydreamLibrary);
            var ClaireMackie_ObeliskLibrary = new VisitorLibrary(ClaireMackie.Id, ObeliskLibrary.Id);
            visitorsLibraries.Add(ClaireMackie_ObeliskLibrary);
            var HaleyBarnard_AlgorithmLibrary = new VisitorLibrary(HaleyBarnard.Id, AlgorithmLibrary.Id);
            visitorsLibraries.Add(HaleyBarnard_AlgorithmLibrary);
            var LeightonAndersen_TempleLibrary = new VisitorLibrary(LeightonAndersen.Id, TempleLibrary.Id);
            visitorsLibraries.Add(LeightonAndersen_TempleLibrary);
            var MattFitzgerald_TempleLibrary = new VisitorLibrary(MattFitzgerald.Id, TempleLibrary.Id);
            visitorsLibraries.Add(MattFitzgerald_TempleLibrary);
            var MattFitzgerald_BeverlyLibrary = new VisitorLibrary(MattFitzgerald.Id, BeverlyLibrary.Id);
            visitorsLibraries.Add(MattFitzgerald_BeverlyLibrary);
            var MattFitzgerald_AmenityLibrary = new VisitorLibrary(MattFitzgerald.Id, AmenityLibrary.Id);
            visitorsLibraries.Add(MattFitzgerald_AmenityLibrary);
            var GinoPearce_ObeliskLibrary = new VisitorLibrary(GinoPearce.Id, ObeliskLibrary.Id);
            visitorsLibraries.Add(GinoPearce_ObeliskLibrary);
            var RehaanYork_AlgorithmLibrary = new VisitorLibrary(RehaanYork.Id, AlgorithmLibrary.Id);
            visitorsLibraries.Add(RehaanYork_AlgorithmLibrary);
            var VerityMorton_DaydreamLibrary = new VisitorLibrary(VerityMorton.Id, DaydreamLibrary.Id);
            visitorsLibraries.Add(VerityMorton_DaydreamLibrary);
            var KadeTravis_DaydreamLibrary = new VisitorLibrary(KadeTravis.Id, DaydreamLibrary.Id);
            visitorsLibraries.Add(KadeTravis_DaydreamLibrary);
            var KhalidHarding_BeverlyLibrary = new VisitorLibrary(KhalidHarding.Id, BeverlyLibrary.Id);
            visitorsLibraries.Add(KhalidHarding_BeverlyLibrary);
            var KhalidHarding_ObeliskLibrary = new VisitorLibrary(KhalidHarding.Id, ObeliskLibrary.Id);
            visitorsLibraries.Add(KhalidHarding_ObeliskLibrary);
            var HasnainKearney_AeosLibrary = new VisitorLibrary(HasnainKearney.Id, AeosLibrary.Id);
            visitorsLibraries.Add(HasnainKearney_AeosLibrary);
            var BeaudenNielsen_ObeliskLibrary = new VisitorLibrary(BeaudenNielsen.Id, ObeliskLibrary.Id);
            visitorsLibraries.Add(BeaudenNielsen_ObeliskLibrary);
            var KellanConroy_AeosLibrary = new VisitorLibrary(KellanConroy.Id, AeosLibrary.Id);
            visitorsLibraries.Add(KellanConroy_AeosLibrary);
            var KellanConroy_ObeliskLibrary = new VisitorLibrary(KellanConroy.Id, ObeliskLibrary.Id);
            visitorsLibraries.Add(KellanConroy_ObeliskLibrary);

            var genres = new List<Genre>();

            var crimeGenre = new Genre("Crime Genre"); 
            genres.Add(crimeGenre);
            var fantasyGenre = new Genre("Fantasy Genre"); 
            genres.Add(fantasyGenre);
            var mysteryGenre = new Genre("Mystery Genre"); 
            genres.Add(mysteryGenre);
            var romanceGenre = new Genre("Romance Genre"); 
            genres.Add(romanceGenre);
            var sciFiGenre = new Genre("Sci-Fi Genre"); 
            genres.Add(sciFiGenre);

            var authors = new List<Author>();

            var PaulSnyder = new Author("Paul Snyder");
            authors.Add(PaulSnyder);
            var KingsleyCraig = new Author("Kingsley Craig");
            authors.Add(KingsleyCraig);
            var MarshallShaw = new Author("Marshall Shaw");
            authors.Add(MarshallShaw);
            var ArnoldMalone = new Author("Arnold Malone");
            authors.Add(ArnoldMalone);
            var AlanMckinney = new Author("Alan Mckinney");
            authors.Add(AlanMckinney);
            var AikenBell = new Author("Aiken Bell");
            authors.Add(AikenBell);
            var BazStone = new Author("Baz Stone");
            authors.Add(BazStone);
            var RayWade = new Author("Ray Wade");
            authors.Add(RayWade);
            var RockyReid = new Author("Rocky Reid");
            authors.Add(RockyReid);
            var BlakeHoyles = new Author("Blake Hoyles");
            authors.Add(BlakeHoyles);
            var WintonStone = new Author("Winton Stone");
            authors.Add(WintonStone);
            var ErnestHodgson = new Author("Ernest Hodgson");
            authors.Add(ErnestHodgson);
            var KitStevenson = new Author("Kit Stevenson");
            authors.Add(KitStevenson);
            var RyanLynch = new Author("Ryan Lynch");
            authors.Add(RyanLynch);
            var DentonFrazier = new Author("Denton Frazier");
            authors.Add(DentonFrazier);

            var releaseForms = new List<ReleaseForm>();

            var hardcoverReleaseForm = new ReleaseForm("Hardcover Release Form");
            releaseForms.Add(hardcoverReleaseForm);
            var paperbackReleaseForm = new ReleaseForm("Paperback Release Form");
            releaseForms.Add(paperbackReleaseForm);
            var premiumReleaseForm = new ReleaseForm("Premium Release Form");

            var books = new List<Book>();

            var TheCase = new Book(@"""The Case""", PaulSnyder.Id, crimeGenre.Id);  
            books.Add(TheCase);
            var Darkside = new Book(@"""Darkside""", KingsleyCraig.Id, crimeGenre.Id); 
            books.Add(Darkside);
            var Mercy = new Book(@"""Mercy""", MarshallShaw.Id, crimeGenre.Id); 
            books.Add(Mercy);
            var BlueLightning = new Book(@"""Blue Lightning""", ArnoldMalone.Id, crimeGenre.Id); 
            books.Add(BlueLightning);
            var TheFrozenDead = new Book(@"""The Frozen Dead""", AlanMckinney.Id, crimeGenre.Id); 
            books.Add(TheFrozenDead);
            var TheHobbit = new Book(@"""The Hobbit""", AikenBell.Id, fantasyGenre.Id); 
            books.Add(TheHobbit); 
            var TheSword = new Book(@"""The Sword""", BazStone.Id, fantasyGenre.Id); 
            books.Add(TheSword);
            var TheLion = new Book(@"""The Lion""", RayWade.Id, fantasyGenre.Id); 
            books.Add(TheLion);
            var TheMystery = new Book(@"""The Mystery""", RockyReid.Id, mysteryGenre.Id); 
            books.Add(TheMystery);
            var TheZone = new Book(@"""The Zone""", BlakeHoyles.Id, mysteryGenre.Id); 
            books.Add(TheZone);
            var LordOfScoundrels = new Book(@"""Lord of Scoundrels""", WintonStone.Id, romanceGenre.Id); 
            books.Add(LordOfScoundrels);
            var Indigo = new Book(@"""Indigo""", ErnestHodgson.Id, romanceGenre.Id); 
            books.Add(Indigo);
            var TheLostWeekend = new Book(@"""The Lost Weekend""", KitStevenson.Id, romanceGenre.Id); 
            books.Add(TheLostWeekend);
            var AtomSmashing = new Book(@"""Atom Smashing""", RyanLynch.Id, sciFiGenre.Id); 
            books.Add(AtomSmashing);
            var FoodChemistry = new Book(@"""Food Chemistry""", DentonFrazier.Id, sciFiGenre.Id); 
            books.Add(FoodChemistry);

            var editions = new List<Edition>();

            var TheCase_First = new Edition("1st Edition", 342);
            editions.Add(TheCase_First);
            var Darkside_First = new Edition("1st Edition", 45);
            editions.Add(Darkside_First);
            var Mercy_First = new Edition("1st Edition", 134);
            editions.Add(Mercy_First);
            var BlueLightning_First = new Edition("1st Edition", 642);
            editions.Add(BlueLightning_First);
            var TheFrozenDead_First = new Edition("1st Edition", 935);
            editions.Add(TheFrozenDead_First);
            var TheHobbit_First = new Edition("1st Edition", 163);
            editions.Add(TheHobbit_First);
            var TheSword_First = new Edition("1st Edition", 735);
            editions.Add(TheSword_First);
            var TheLion_First = new Edition("1st Edition", 157);
            editions.Add(TheLion_First);
            var TheMystery_First = new Edition("1st Edition", 246);
            editions.Add(TheMystery_First);
            var TheZone_First = new Edition("1st Edition", 512);
            editions.Add(TheZone_First);
            var LordOfScoundrels_First = new Edition("1st Edition", 423);
            editions.Add(LordOfScoundrels_First);
            var Indigo_First = new Edition("1st Edition", 431);
            editions.Add(Indigo_First);
            var TheLostWeekend_First = new Edition("1st Edition", 223);
            editions.Add(TheLostWeekend_First);
            var AtomSmashing_First = new Edition("1st Edition", 192);
            editions.Add(AtomSmashing_First);
            var AtomSmashing_Second = new Edition("2nd Edition", 200);
            editions.Add(AtomSmashing_Second);
            var FoodChemistry_First = new Edition("1st Edition", 312);
            editions.Add(FoodChemistry_First);
            var FoodChemistry_Second = new Edition("2nd Edition", 317);
            editions.Add(FoodChemistry_Second);
            var FoodChemistry_Third = new Edition("3rd Edition", 330);
            editions.Add(FoodChemistry_Third);

            var bookCopies = new List<BookCopy>();

            var TheCase_First1 = new BookCopy(TheCase.Id, TheCase_First.Id, hardcoverReleaseForm.Id); 
            bookCopies.Add(TheCase_First1);
            var TheCase_First2 = new BookCopy(TheCase.Id, TheCase_First.Id, hardcoverReleaseForm.Id); 
            bookCopies.Add(TheCase_First2);
            var Darkside_First1 = new BookCopy(Darkside.Id, Darkside_First.Id, hardcoverReleaseForm.Id); 
            bookCopies.Add(Darkside_First1);
            var Darkside_First2 = new BookCopy(Darkside.Id, Darkside_First.Id, hardcoverReleaseForm.Id); 
            bookCopies.Add(Darkside_First2);
            var Mercy_First1 = new BookCopy(Mercy.Id, Mercy_First.Id, hardcoverReleaseForm.Id); 
            bookCopies.Add(Mercy_First1);
            var Mercy_First2 = new BookCopy(Mercy.Id, Mercy_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(Mercy_First2);
            var Mercy_First3 = new BookCopy(Mercy.Id, Mercy_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(Mercy_First3);
            var BlueLightning_First1 = new BookCopy(BlueLightning.Id, BlueLightning_First.Id, paperbackReleaseForm.Id);
            bookCopies.Add(BlueLightning_First1);
            var BlueLightning_First2 = new BookCopy(BlueLightning.Id, BlueLightning_First.Id, paperbackReleaseForm.Id);
            bookCopies.Add(BlueLightning_First2);
            var TheFrozenDead_First1 = new BookCopy(TheFrozenDead.Id, TheFrozenDead_First.Id, paperbackReleaseForm.Id);
            bookCopies.Add(TheFrozenDead_First1);
            var TheHobbit_First1 = new BookCopy(TheHobbit.Id, TheHobbit_First.Id, premiumReleaseForm.Id);
            bookCopies.Add(TheHobbit_First1);
            var TheHobbit_First2 = new BookCopy(TheHobbit.Id, TheHobbit_First.Id, premiumReleaseForm.Id);
            bookCopies.Add(TheHobbit_First2);
            var TheSword_First1 = new BookCopy(TheSword.Id, TheSword_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(TheSword_First1);
            var TheLion_First1 = new BookCopy(TheLion.Id, TheLion_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(TheLion_First1);
            var TheMystery_First1 = new BookCopy(TheMystery.Id, TheMystery_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(TheMystery_First1);
            var TheMystery_First2 = new BookCopy(TheMystery.Id, TheMystery_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(TheMystery_First2);
            var TheZone_First1 = new BookCopy(TheZone.Id, TheZone_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(TheZone_First1);
            var TheZone_First2 = new BookCopy(TheZone.Id, TheZone_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(TheZone_First2);
            var LordOfScoundrels_First1 = new BookCopy(LordOfScoundrels.Id, LordOfScoundrels_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(LordOfScoundrels_First1);
            var Indigo_First1 = new BookCopy(Indigo.Id, Indigo_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(Indigo_First1);
            var Indigo_First2 = new BookCopy(Indigo.Id, Indigo_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(Indigo_First2);
            var TheLostWeekend_First1 = new BookCopy(TheLostWeekend.Id, TheLostWeekend_First.Id, premiumReleaseForm.Id);
            bookCopies.Add(TheLostWeekend_First1);
            var TheLostWeekend_First2 = new BookCopy(TheLostWeekend.Id, TheLostWeekend_First.Id, premiumReleaseForm.Id);
            bookCopies.Add(TheLostWeekend_First2);
            var TheLostWeekend_First3 = new BookCopy(TheLostWeekend.Id, TheLostWeekend_First.Id, premiumReleaseForm.Id);
            bookCopies.Add(TheLostWeekend_First3);
            var AtomSmashing_First1 = new BookCopy(AtomSmashing.Id, AtomSmashing_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(AtomSmashing_First1);
            var AtomSmashing_First2 = new BookCopy(AtomSmashing.Id, AtomSmashing_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(AtomSmashing_First2);
            var AtomSmashing_Second1 = new BookCopy(AtomSmashing.Id, AtomSmashing_Second.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(AtomSmashing_Second1);
            var AtomSmashing_Second2 = new BookCopy(AtomSmashing.Id, AtomSmashing_Second.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(AtomSmashing_Second2);
            var FoodChemistry_First1 = new BookCopy(FoodChemistry.Id, FoodChemistry_First.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(FoodChemistry_First1);
            var FoodChemistry_Second1 = new BookCopy(FoodChemistry.Id, FoodChemistry_Second.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(FoodChemistry_Second1);
            var FoodChemistry_Second2 = new BookCopy(FoodChemistry.Id, FoodChemistry_Second.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(FoodChemistry_Second2);
            var FoodChemistry_Third1 = new BookCopy(FoodChemistry.Id, FoodChemistry_Third.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(FoodChemistry_Third1);
            var FoodChemistry_Third2 = new BookCopy(FoodChemistry.Id, FoodChemistry_Third.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(FoodChemistry_Third2);
            var FoodChemistry_Third3 = new BookCopy(FoodChemistry.Id, FoodChemistry_Third.Id, hardcoverReleaseForm.Id);
            bookCopies.Add(FoodChemistry_Third3);

            var booksCopiesLibraries = new List<BookCopyLibrary>();

            var TempleLibrary_TheCase_First1 = new BookCopyLibrary(TheCase_First1.Id, TempleLibrary.Id);
            booksCopiesLibraries.Add(TempleLibrary_TheCase_First1);
            var TempleLibrary_TheLion_First1 = new BookCopyLibrary(TheLion_First1.Id, TempleLibrary.Id);
            booksCopiesLibraries.Add(TempleLibrary_TheLion_First1);
            var TempleLibrary_Indigo_First1 = new BookCopyLibrary(Indigo_First1.Id, TempleLibrary.Id);
            booksCopiesLibraries.Add(TempleLibrary_Indigo_First1);
            var TempleLibrary_AtomSmashing_First1 = new BookCopyLibrary(AtomSmashing_First1.Id, TempleLibrary.Id);
            booksCopiesLibraries.Add(TempleLibrary_AtomSmashing_First1);
            var ObeliskLibrary_TheCase_First2 = new BookCopyLibrary(TheCase_First2.Id, ObeliskLibrary.Id);
            booksCopiesLibraries.Add(ObeliskLibrary_TheCase_First2);
            var ObeliskLibrary_TheLostWeekend_First1 = new BookCopyLibrary(TheLostWeekend_First1.Id, ObeliskLibrary.Id);
            booksCopiesLibraries.Add(ObeliskLibrary_TheLostWeekend_First1);
            var ObeliskLibrary_TheLostWeekend_First2 = new BookCopyLibrary(TheLostWeekend_First2.Id, ObeliskLibrary.Id);
            booksCopiesLibraries.Add(ObeliskLibrary_TheLostWeekend_First2);
            var ObeliskLibrary_AtomSmashing_First2 = new BookCopyLibrary(AtomSmashing_First2.Id, ObeliskLibrary.Id);
            booksCopiesLibraries.Add(ObeliskLibrary_AtomSmashing_First2);
            var ObeliskLibrary_TheZone_First1 = new BookCopyLibrary(TheZone_First1.Id, ObeliskLibrary.Id);
            booksCopiesLibraries.Add(ObeliskLibrary_TheZone_First1);
            var AlgorithmLibrary_Darkside_First1 = new BookCopyLibrary(Darkside_First1.Id, AlgorithmLibrary.Id);
            booksCopiesLibraries.Add(AlgorithmLibrary_Darkside_First1);
            var AlgorithmLibrary_FoodChemistry_First1 = new BookCopyLibrary(FoodChemistry_First1.Id, AlgorithmLibrary.Id);
            booksCopiesLibraries.Add(AlgorithmLibrary_FoodChemistry_First1);
            var AlgorithmLibrary_FoodChemistry_Second1 = new BookCopyLibrary(FoodChemistry_Second1.Id, AlgorithmLibrary.Id);
            booksCopiesLibraries.Add(AlgorithmLibrary_FoodChemistry_Second1);
            var AlgorithmLibrary_FoodChemistry_Second2 = new BookCopyLibrary(FoodChemistry_Second2.Id, AlgorithmLibrary.Id);
            booksCopiesLibraries.Add(AlgorithmLibrary_FoodChemistry_Second2);
            var AlgorithmLibrary_Indigo_First2 = new BookCopyLibrary(Indigo_First2.Id, AlgorithmLibrary.Id);
            booksCopiesLibraries.Add(AlgorithmLibrary_Indigo_First2);
            var DaydreamLibrary_TheHobbit_First1 = new BookCopyLibrary(TheHobbit_First1.Id, DaydreamLibrary.Id);
            booksCopiesLibraries.Add(DaydreamLibrary_TheHobbit_First1);
            var DaydreamLibrary_Mercy_First1 = new BookCopyLibrary(Mercy_First1.Id, DaydreamLibrary.Id);
            booksCopiesLibraries.Add(DaydreamLibrary_Mercy_First1);
            var DaydreamLibrary_BlueLightning_First1 = new BookCopyLibrary(BlueLightning_First1.Id, DaydreamLibrary.Id);
            booksCopiesLibraries.Add(DaydreamLibrary_BlueLightning_First1);
            var DaydreamLibrary_FoodChemistry_Third1 = new BookCopyLibrary(FoodChemistry_Third1.Id, DaydreamLibrary.Id);
            booksCopiesLibraries.Add(DaydreamLibrary_FoodChemistry_Third1);
            var DaydreamLibrary_FoodChemistry_Third2 = new BookCopyLibrary(FoodChemistry_Third2.Id, DaydreamLibrary.Id);
            booksCopiesLibraries.Add(DaydreamLibrary_FoodChemistry_Third2);
            var DaydreamLibrary_FoodChemistry_Third3 = new BookCopyLibrary(FoodChemistry_Third3.Id, DaydreamLibrary.Id);
            booksCopiesLibraries.Add(DaydreamLibrary_FoodChemistry_Third3);
            var AmenityLibrary_AtomSmashing_Second1 = new BookCopyLibrary(AtomSmashing_Second1.Id, AmenityLibrary.Id);
            booksCopiesLibraries.Add(AmenityLibrary_AtomSmashing_Second1);
            var AmenityLibrary_AtomSmashing_Second2 = new BookCopyLibrary(AtomSmashing_Second2.Id, AmenityLibrary.Id);
            booksCopiesLibraries.Add(AmenityLibrary_AtomSmashing_Second2);
            var AmenityLibrary_Mercy_First2 = new BookCopyLibrary(Mercy_First2.Id, AmenityLibrary.Id);
            booksCopiesLibraries.Add(AmenityLibrary_Mercy_First2);
            var AmenityLibrary_BlueLightning_First2 = new BookCopyLibrary(BlueLightning_First2.Id, AmenityLibrary.Id);
            booksCopiesLibraries.Add(AmenityLibrary_BlueLightning_First2);
            var BeverlyLibrary_TheMystery_First1 = new BookCopyLibrary(TheMystery_First1.Id, BeverlyLibrary.Id);
            booksCopiesLibraries.Add(BeverlyLibrary_TheMystery_First1);
            var BeverlyLibrary_TheMystery_First2 = new BookCopyLibrary(TheMystery_First2.Id, BeverlyLibrary.Id);
            booksCopiesLibraries.Add(BeverlyLibrary_TheMystery_First2);
            var BeverlyLibrary_TheFrozenDead_First1 = new BookCopyLibrary(TheFrozenDead_First1.Id, BeverlyLibrary.Id);
            booksCopiesLibraries.Add(BeverlyLibrary_TheFrozenDead_First1);
            var BeverlyLibrary_Mercy_First3 = new BookCopyLibrary(Mercy_First3.Id, BeverlyLibrary.Id);
            booksCopiesLibraries.Add(BeverlyLibrary_Mercy_First3);
            var BeverlyLibrary_TheHobbit_First2 = new BookCopyLibrary(TheHobbit_First2.Id, BeverlyLibrary.Id);
            booksCopiesLibraries.Add(BeverlyLibrary_TheHobbit_First2);
            var AeosLibrary_TheZone_First2 = new BookCopyLibrary(TheZone_First2.Id, AeosLibrary.Id);
            booksCopiesLibraries.Add(AeosLibrary_TheZone_First2);
            var AeosLibrary_LordOfScoundrels_First1 = new BookCopyLibrary(LordOfScoundrels_First1.Id, AeosLibrary.Id);
            booksCopiesLibraries.Add(AeosLibrary_LordOfScoundrels_First1);
            var AeosLibrary_TheLostWeekend_First3 = new BookCopyLibrary(TheLostWeekend_First3.Id, AeosLibrary.Id);
            booksCopiesLibraries.Add(AeosLibrary_TheLostWeekend_First3);
            var AeosLibrary_TheSword_First1 = new BookCopyLibrary(TheSword_First1.Id, AeosLibrary.Id);
            booksCopiesLibraries.Add(AeosLibrary_TheSword_First1);

            var visits = new List<Visit>
            {
                new Visit(SanjeevAdams_TempleLibrary.Id, TempleLibrary_TheCase_First1.Id, new DateTime(2012, 1, 20), new DateTime(2012, 1, 25)),
                new Visit(LeightonAndersen_TempleLibrary.Id, TempleLibrary_TheCase_First1.Id, new DateTime(2012, 2, 14), new DateTime(2012, 3, 20)),
                new Visit(MattFitzgerald_TempleLibrary.Id, TempleLibrary_Indigo_First1.Id, new DateTime(2013, 4, 20), new DateTime(2013, 4, 21)),
                new Visit(MattFitzgerald_TempleLibrary.Id, TempleLibrary_TheLion_First1.Id, new DateTime(2003, 3, 2), new DateTime(2003, 4, 5)),
                new Visit(MattFitzgerald_TempleLibrary.Id, TempleLibrary_AtomSmashing_First1.Id, new DateTime(2010, 3, 2), new DateTime(2010, 4, 5)),
                new Visit(SanjeevAdams_DaydreamLibrary.Id, DaydreamLibrary_FoodChemistry_Third1.Id, new DateTime(2005, 5, 7), new DateTime(2005, 5, 15)),
                new Visit(VerityMorton_DaydreamLibrary.Id, DaydreamLibrary_FoodChemistry_Third2.Id, new DateTime(2012, 3, 24), new DateTime(2012, 3, 27)),
                new Visit(KadeTravis_DaydreamLibrary.Id, DaydreamLibrary_FoodChemistry_Third1.Id, new DateTime(2012, 4, 13), new DateTime(2012, 4, 20)),
                new Visit(KadeTravis_DaydreamLibrary.Id, DaydreamLibrary_FoodChemistry_Third3.Id, new DateTime(2012, 6, 4), new DateTime(2012, 6, 12)),
                new Visit(SanjeevAdams_DaydreamLibrary.Id, DaydreamLibrary_TheHobbit_First1.Id, new DateTime(2012, 7, 20), new DateTime(2012, 8, 9)),
                new Visit(VerityMorton_DaydreamLibrary.Id, DaydreamLibrary_Mercy_First1.Id, new DateTime(2012, 7, 10), new DateTime(2012, 7, 15)),
                new Visit(KadeTravis_DaydreamLibrary.Id, DaydreamLibrary_BlueLightning_First1.Id, new DateTime(2012, 8, 10), new DateTime(2012, 8, 18)),
                new Visit(KadeTravis_DaydreamLibrary.Id, DaydreamLibrary_TheHobbit_First1.Id, new DateTime(2012, 9, 10), new DateTime(2012, 9, 12)),
                new Visit(VerityMorton_DaydreamLibrary.Id, DaydreamLibrary_TheHobbit_First1.Id, new DateTime(2012, 10, 10), new DateTime(2012, 10, 19)),
                new Visit(ClaireMackie_ObeliskLibrary.Id, ObeliskLibrary_TheCase_First2.Id, new DateTime(2021, 1, 1)),
                new Visit(KhalidHarding_ObeliskLibrary.Id, ObeliskLibrary_TheLostWeekend_First1.Id, new DateTime(2015, 3, 8), new DateTime(2015, 3, 15)),
                new Visit(BeaudenNielsen_ObeliskLibrary.Id, ObeliskLibrary_TheLostWeekend_First1.Id, new DateTime(2015, 4, 8), new DateTime(2015, 4, 15)),
                new Visit(KellanConroy_ObeliskLibrary.Id, ObeliskLibrary_TheLostWeekend_First2.Id, new DateTime(2015, 5, 15), new DateTime(2015, 5, 15)),
                new Visit(ClaireMackie_ObeliskLibrary.Id, ObeliskLibrary_TheLostWeekend_First2.Id, new DateTime(2015, 6, 15), new DateTime(2015, 6, 15)),
                new Visit(HaleyBarnard_AlgorithmLibrary.Id, AlgorithmLibrary_Darkside_First1.Id, new DateTime(2016, 1, 4), new DateTime(2016, 1, 12)),
                new Visit(RehaanYork_AlgorithmLibrary.Id, AlgorithmLibrary_FoodChemistry_First1.Id, new DateTime(2016, 2, 4), new DateTime(2016, 2, 12)),
                new Visit(HaleyBarnard_AlgorithmLibrary.Id, AlgorithmLibrary_FoodChemistry_Second1.Id, new DateTime(2016, 4, 4), new DateTime(2016, 4, 12)),
                new Visit(HaleyBarnard_AlgorithmLibrary.Id, AlgorithmLibrary_FoodChemistry_Second2.Id, new DateTime(2016, 4, 5), new DateTime(2016, 4, 20)),
                new Visit(RehaanYork_AlgorithmLibrary.Id, AlgorithmLibrary_Indigo_First2.Id, new DateTime(2011, 6, 4), new DateTime(2011, 6, 15)),
                new Visit(MattFitzgerald_BeverlyLibrary.Id, BeverlyLibrary_TheMystery_First1.Id, new DateTime(2011, 6, 3), new DateTime(2011, 6, 5)),
                new Visit(KhalidHarding_BeverlyLibrary.Id, BeverlyLibrary_TheMystery_First2.Id, new DateTime(2011, 6, 12), new DateTime(2011, 6, 15)),
                new Visit(MattFitzgerald_BeverlyLibrary.Id, BeverlyLibrary_TheFrozenDead_First1.Id, new DateTime(2011, 6, 9), new DateTime(2011, 6, 23)),
                new Visit(KhalidHarding_BeverlyLibrary.Id, BeverlyLibrary_Mercy_First3.Id, new DateTime(2011, 7, 4), new DateTime(2011, 7, 15)),
                new Visit(MattFitzgerald_BeverlyLibrary.Id, BeverlyLibrary_TheHobbit_First2.Id, new DateTime(2011, 8, 4), new DateTime(2011, 8, 15)),
                new Visit(MattFitzgerald_BeverlyLibrary.Id, BeverlyLibrary_TheHobbit_First2.Id, new DateTime(2011, 6, 17), new DateTime(2011, 6, 25)),
                new Visit(HasnainKearney_AeosLibrary.Id, AeosLibrary_TheZone_First2.Id, new DateTime(2016, 6, 17), new DateTime(2016, 6, 25)),
                new Visit(KellanConroy_AeosLibrary.Id, AeosLibrary_TheZone_First2.Id, new DateTime(2017, 6, 17), new DateTime(2017, 6, 25)),
                new Visit(HasnainKearney_AeosLibrary.Id, AeosLibrary_TheSword_First1.Id, new DateTime(2018, 6, 17)),
                new Visit(HasnainKearney_AeosLibrary.Id, AeosLibrary_LordOfScoundrels_First1.Id, new DateTime(2019, 6, 17), new DateTime(2019, 6, 25)),
                new Visit(KellanConroy_AeosLibrary.Id, AeosLibrary_LordOfScoundrels_First1.Id, new DateTime(2019, 6, 26), new DateTime(2019, 7, 27)),
                new Visit(KellanConroy_AeosLibrary.Id, AeosLibrary_TheLostWeekend_First3.Id, new DateTime(2019, 6, 1), new DateTime(2019, 6, 5))
            };            

            return (visitorsLibraries, visitors, libraries, districts, Chester, books, genres, editions, bookCopies, booksCopiesLibraries,
                    visits);
        }
    }
}

//· Вывести список названий библиотек.
//· Сгруппировать библиотеки по районам и вывести ввиде:
//«Название района1» («Количество библиотек в районе»)
//«Библиотека1»
//«Библиотека2»
//«Название района2» («Количество библиотек в районе»)
//«Библиотека3»
//«Библиотека4»
//«Библиотека5»
//«Количество библиотек в районе» рассчитывается методом Count() на каждой группировке.
//· Вывести список районов, где нет ни одной библиотеки
//· Вывести всех посетителей всех библиотек
//· Вывести всех посетителей с группировкой по библиотеке
//· Вывести всех посетителей с группировкой по району
//· Вывести районы и количество посетителей. Отсортируй по количеству посетителей по убыванию.
//· Для каждого жанра посчитать количество книг в нем (по всем библиотекам (без группировки) и по каждой библиотеке (с группировкой по библиотеке))
//· Для каждого жанра посчитать количество экземпляров книг
//· Вывести самую читаемую книгу по всем библиотекам
//· Вывести самую читаемую книгу для каждой библиотеки
//· Вывести самый читаемый жанр по всем библиотекам
//· Вывести самый читаемый жанр для каждой библиотеки
//· Вывести топ 10 самых читаемых книг по каждому жанру (по всем библиотекам + по каждой в отдельности)
//· Повторить все 5 предыдущих пункта, но с разбивкой по месяцам
//· То же самое, но с разбивкой по годам
//· Вывести всех посетителей, кто превысил время чтения книги
//  (давай сделаем этот параметр свой у каждой библиотеки).
//  В качестве усложнения его можно рассчитывать исходя из жанра 
//  (к примеру, для редкой периодики -50% недели от стандарта, для старой классики +100%)
//· Вывести книги по библиотекам, все экземпляры которых сейчас на руках у посетителей
//· Вывести топ 10 самых долгочитаемых книг по каждому жанру (по каждой библиотеке и по всем библиотекам)
