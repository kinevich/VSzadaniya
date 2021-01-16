using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var Chester = new City("Chester");

            var districts = new List<District>()
            {
                new District("Blood Plaza", Chester.Id),
                new District("North Lownerd", Chester.Id),
                new District("Wacit Grove", Chester.Id),
                new District("Butalp North", Chester.Id),
                new District("Cherrift Wood", Chester.Id)
            };

            var libraries = new List<Library>
            {
                new Library("Saturninity Library", districts[0].Id),
                new Library("Pioneer Library", districts[0].Id),
                new Library("Open Book Library", districts[0].Id),
                new Library("Obelisk Library", districts[0].Id),
                new Library("Algorithm Library", districts[0].Id),
                new Library("National Public Library", districts[1].Id),
                new Library("Daydream Library", districts[1].Id),
                new Library("Plainfield Library", districts[1].Id),
                new Library("Amenity Library", districts[1].Id),
                new Library("Beverly Library", districts[3].Id),
                new Library("Aeos Library", districts[4].Id),
                new Library("Rainbow Library", districts[4].Id),
                new Library("Repose Library", districts[4].Id),
                new Library("Valley Library", districts[4].Id),
                new Library("Capitol Library", districts[4].Id),
                new Library("Zenith Library", districts[4].Id)
            };
            // 16 libraries, 5 districts
        
            ListLibraryNames(libraries);
            GroupLibrariesByDistricts(libraries, districts);
            ListDistrictsDontHaveLibraries(libraries, districts);
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

        private static void GroupLibrariesByDistricts(List<Library> libraries,
                                                      List<District> districts)
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

        private static void ListDistrictsDontHaveLibraries(List<Library> libraries,
                                                           List<District> districts)
        {
            var query = from district in districts
                        where !libraries.Any(l => l.DistrictId == district.Id)
                        select district.Name;
                        
            foreach (var disName in query)
            {
                Console.WriteLine(disName);
            }
            Console.WriteLine();
        }
    }
}
