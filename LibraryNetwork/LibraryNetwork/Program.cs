using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var BloodPlazaLabs = new List<Library>
            {
                new Library("Saturninity Library"), new Library("Pioneer Library"),
                new Library("Open Book Library"), new Library("Obelisk Library"),
                new Library("Algorithm Library")
            };

            var NorthLownerdLabs = new List<Library>
            {
                new Library("National Public Library"), new Library("Daydream Library"),
                new Library("Plainfield Library"), new Library("Amenity Library"),
            };

            var WacitGroveLabs = new List<Library>();

            var ButalpNorthLabs = new List<Library>
            {
                new Library("National Public Library")
            };

            var CherriftWoodLabs = new List<Library>
            {
                new Library("Aeos Library"), new Library("Rainbow Library"),
                new Library("Repose Library"), new Library("Valley Library"),
                new Library("Capitol Library"), new Library("Zenith Library"),
            };

            var ChesterDistricts = new List<District> 
            {
                new District("Blood Plaza", BloodPlazaLabs),
                new District("North Lownerd", NorthLownerdLabs),
                new District("Wacit Grove", WacitGroveLabs),
                new District("Butalp North", ButalpNorthLabs),
                new District("Cherrift Wood", CherriftWoodLabs)
            };

            var Chester = new City("Chester", ChesterDistricts);

            // 16 libraries, 5 districts
        
            Chester.ListLibraryNames();
            Chester.GroupLibrariesByDistricts();
        }
    }

    class City
    {
        public Guid Id { get; }

        public string Name { get; }

        public List<District> Districts { get; }

        public City(string name, List<District> districts)
        {
            Id = Guid.NewGuid();
            Name = name;
            Districts = districts;
        }

        public void ListLibraryNames()
        {
            Console.WriteLine("*****List Library Names*****");

            var query = from district in Districts
                        from library in district.Libraries
                        select library.Name;

            int i = 0;
            foreach (var libraryName in query)
            {
                ++i;
                Console.WriteLine($"{i}. {libraryName}");
            }
            Console.WriteLine();
        }

        public void GroupLibrariesByDistricts()
        {
            Console.WriteLine("*****Group Libraries By Districts*****");

            var query = from district in Districts
                        from library in district.Libraries
                        group library by district.Name;

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Key}({group.Count()} libraries)");
                foreach (var library in group)
                {
                    Console.WriteLine(library.Name);
                }
            }
            Console.WriteLine();
        }
    }

    class District
    {
        public Guid Id { get; }

        public string Name { get; }

        public List<Library> Libraries { get; }

        public District(string name, List<Library> libraries)
        {
            Id = Guid.NewGuid();
            Libraries = libraries;
            Name = name;
        }
    }

    class Library
    {
        public Guid Id { get; }

        public string Name { get; }

        public Library(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

    class Book
    {
        public Guid Id { get; }

        public string Title { get; }

        public Guid AuthorId { get; }

        public Guid GenreId { get; }

        public Book(string title, Author author, Genre genre)
        {
            Id = Guid.NewGuid();
            AuthorId = author.Id;
            GenreId = genre.Id;
            Title = title;
        }
    }

    class BookCopy
    {
        public Guid Id { get; }

        public Guid BookId { get; }

        public Guid EditionId { get; }

        public Guid ReleaseFormId { get; }

        public BookCopy(Book book, Edition edition, ReleaseForm releaseForm)
        {
            Id = Guid.NewGuid();
            BookId = book.Id;
            EditionId = edition.Id;
            ReleaseFormId = releaseForm.Id;
        }

    }

    class Edition
    {
        public Guid Id { get; }

        public int Number { get; }

        public int Pages { get; }

        public Edition(int number, int pages)
        {
            Id = Guid.NewGuid();
            Number = number;
            Pages = pages;
        }
    }

    class Genre
    {
        public Guid Id { get; }

        public string Name { get; }

        public Genre(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

    class ReleaseForm
    {
        public Guid Id { get; }

        public string Name { get; }

        public ReleaseForm(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

    class Author
    {
        public Guid Id { get; }

        public string Name { get; }

        public Author(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
