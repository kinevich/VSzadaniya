using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            (List<VisitorLibrary> visitorsLibrary, List<Visitor> visitors, List<Library> libraries,
             List<District> districts, City city) = GetData();

            // 16 libraries, 5 districts

            //ListLibraryNames(libraries);
            //ListGroupedLibrariesByDistricts(libraries, districts);
            //ListDistrictsDontHaveLibraries(libraries, districts);
            //ListAllVisitors(libraries, visitorsLibrary, visitors);
            //ListVisitorsByLibraries(libraries, visitors, visitorsLibrary);
            ListVisitorsByDistricts(libraries, districts, visitors, visitorsLibrary);
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

        private static void ListGroupedLibrariesByDistricts(List<Library> libraries,
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

        private static void ListAllVisitors(List<Library> libraries, List<VisitorLibrary> visitorsLibrary, 
                                            List<Visitor> visitors)
                                            
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
                                                       List<VisitorLibrary> visitorsLibrary)

        {
            var query = from library in libraries
                        join visitorLibrary in visitorsLibrary on
                        library.Id equals visitorLibrary.LibraryId into visLibGroup
                        select new { LibraryName = library.Name, visitorsLibrary = visLibGroup };


            foreach(var group in query)
            {
                Console.WriteLine($"{group.LibraryName}:");

                foreach (var visitorLibrary in group.visitorsLibrary)
                {
                    Visitor visitor = visitors.Find(v => v.Id == visitorLibrary.VisitorId);
                    Console.WriteLine($" {visitor.Name}");
                }
            }
            Console.WriteLine();
        }

        private static void ListVisitorsByDistricts(List<Library> libraries, List<District> districts, 
                                                    List<Visitor> visitors, 
                                                    List<VisitorLibrary> visitorsLibrary)
        {
            var visitorsByDistricts = from district in districts
                                      join library in libraries on
                                      district.Id equals library.DistrictId into libGroup
                                      select new
                                      {
                                          DistrictName = district.Name,
                                          VisitorsLibrary =
                                          from library in libGroup
                                          join visitorLibrary in visitorsLibrary on
                                          library.Id equals visitorLibrary.LibraryId
                                          select visitorLibrary.VisitorId
                                      };
                                      
            foreach (var group in visitorsByDistricts)
            {
                Console.WriteLine($"{group.DistrictName}:");
                foreach (var visitorLibraryId in group.VisitorsLibrary)
                {
                    Visitor visitor = visitors.Find(v => v.Id == visitorLibraryId);
                    Console.WriteLine($" {visitor.Name}");
                }
            }
            Console.WriteLine();
        }

        private static (List<VisitorLibrary> visitorsLibrary, List<Visitor> visitors, 
                        List<Library> libraries, List<District> districts, City city) GetData()

        {
            var Chester = new City("Chester");

            var districts = new List<District>();

            var BloodPlaza = new District("Blood Plaza", Chester.Id);
            districts.Add(BloodPlaza);
            var NorthLownerd = new District("North Lownerd", Chester.Id);
            districts.Add(NorthLownerd);
            var WacitGrove = new District("WacitGrove", Chester.Id);
            districts.Add(WacitGrove);
            var ButalpNorth = new District("ButalpNort", Chester.Id);
            districts.Add(ButalpNorth);
            var CherriftWood = new District("CherriftWood", Chester.Id);
            districts.Add(CherriftWood);

            var libraries = new List<Library>();

            Library TempleLibrary = new Library("Temple Library", BloodPlaza.Id); // 3-1-1-1+
            libraries.Add(TempleLibrary);
            Library ObeliskLibrary = new Library("Obelisk Library", BloodPlaza.Id); // 5-1-1-1-1-1+
            libraries.Add(ObeliskLibrary);
            Library AlgorithmLibrary = new Library("Algorithm Library", BloodPlaza.Id); //2-1-1+
            libraries.Add(AlgorithmLibrary);
            Library DaydreamLibrary = new Library("Daydream Library", NorthLownerd.Id); //3-1-1-1+
            libraries.Add(DaydreamLibrary);
            Library AmenityLibrary = new Library("Amenity Library", NorthLownerd.Id); //1-1+
            libraries.Add(AmenityLibrary);
            Library BeverlyLibrary = new Library("Beverly Library", ButalpNorth.Id); // 2-1-1+
            libraries.Add(BeverlyLibrary);
            Library AeosLibrary = new Library("Aeos Library", CherriftWood.Id); // 2-1-1+
            libraries.Add(AeosLibrary);

            var visitors = new List<Visitor>();

            Visitor SanjeevAdams = new Visitor("Sanjeev Adams"); // 2 +
            visitors.Add(SanjeevAdams);
            Visitor ClaireMackie = new Visitor("Claire Mackie"); //  +
            visitors.Add(ClaireMackie);
            Visitor HaleyBarnard = new Visitor("Haley Barnard");// +
            visitors.Add(HaleyBarnard);
            Visitor LeightonAndersen = new Visitor("Leighton Andersen"); // +
            visitors.Add(LeightonAndersen);
            Visitor MattFitzgerald = new Visitor("Matt Fitzgerald"); // 3 +
            visitors.Add(MattFitzgerald);
            Visitor GinoPearce = new Visitor("Gino Pearce"); //+
            visitors.Add(GinoPearce);
            Visitor RehaanYork = new Visitor("Rehaan York");//+
            visitors.Add(RehaanYork);
            Visitor VerityMorton = new Visitor("Verity Morton"); // +
            visitors.Add(VerityMorton);
            Visitor KadeTravis = new Visitor("Kade Travis"); // +
            visitors.Add(KadeTravis);
            Visitor KhalidHarding = new Visitor("Khalid Harding"); // 2 +
            visitors.Add(KhalidHarding);
            Visitor HasnainKearney = new Visitor("Hasnain Kearney");
            visitors.Add(HasnainKearney);
            Visitor BeaudenNielsen = new Visitor("Beauden Nielsen");
            visitors.Add(BeaudenNielsen);
            Visitor KellanConroy = new Visitor("Kellan Conroy"); // 2
            visitors.Add(KellanConroy);

            var visitorsLibrary = new List<VisitorLibrary>();

            var SanjeevAdams_TempleLibrary = new VisitorLibrary(SanjeevAdams.Id, TempleLibrary.Id);
            visitorsLibrary.Add(SanjeevAdams_TempleLibrary);
            var SanjeevAdams_DaydreamLibrary = new VisitorLibrary(SanjeevAdams.Id, DaydreamLibrary.Id);
            visitorsLibrary.Add(SanjeevAdams_DaydreamLibrary);
            var ClaireMackie_ObeliskLibrary = new VisitorLibrary(ClaireMackie.Id, ObeliskLibrary.Id);
            visitorsLibrary.Add(ClaireMackie_ObeliskLibrary);
            var HaleyBarnard_AlgorithmLibrary = new VisitorLibrary(HaleyBarnard.Id, AlgorithmLibrary.Id);
            visitorsLibrary.Add(HaleyBarnard_AlgorithmLibrary);
            var LeightonAndersen_TempleLibrary = new VisitorLibrary(LeightonAndersen.Id, TempleLibrary.Id);
            visitorsLibrary.Add(LeightonAndersen_TempleLibrary);
            var MattFitzgerald_TempleLibrary = new VisitorLibrary(MattFitzgerald.Id, TempleLibrary.Id);
            visitorsLibrary.Add(MattFitzgerald_TempleLibrary);
            var MattFitzgerald_BeverlyLibrary = new VisitorLibrary(MattFitzgerald.Id, BeverlyLibrary.Id);
            visitorsLibrary.Add(MattFitzgerald_BeverlyLibrary);
            var MattFitzgerald_AmenityLibrary = new VisitorLibrary(MattFitzgerald.Id, AmenityLibrary.Id);
            visitorsLibrary.Add(MattFitzgerald_AmenityLibrary);
            var GinoPearce_ObeliskLibrary = new VisitorLibrary(GinoPearce.Id, ObeliskLibrary.Id);
            visitorsLibrary.Add(GinoPearce_ObeliskLibrary);
            var RehaanYork_AlgorithmLibrary = new VisitorLibrary(RehaanYork.Id, AlgorithmLibrary.Id);
            visitorsLibrary.Add(RehaanYork_AlgorithmLibrary);
            var VerityMorton_DaydreamLibrary = new VisitorLibrary(VerityMorton.Id, DaydreamLibrary.Id);
            visitorsLibrary.Add(VerityMorton_DaydreamLibrary);
            var KadeTravis_DaydreamLibrary = new VisitorLibrary(KadeTravis.Id, DaydreamLibrary.Id);
            visitorsLibrary.Add(KadeTravis_DaydreamLibrary);
            var KhalidHarding_BeverlyLibrary = new VisitorLibrary(KhalidHarding.Id, BeverlyLibrary.Id);
            visitorsLibrary.Add(KhalidHarding_BeverlyLibrary);
            var KhalidHarding_ObeliskLibrary = new VisitorLibrary(KhalidHarding.Id, ObeliskLibrary.Id);
            visitorsLibrary.Add(KhalidHarding_ObeliskLibrary);
            var HasnainKearney_AeosLibrary = new VisitorLibrary(HasnainKearney.Id, AeosLibrary.Id);
            visitorsLibrary.Add(HasnainKearney_AeosLibrary);
            var BeaudenNielsen_ObeliskLibrary = new VisitorLibrary(BeaudenNielsen.Id, ObeliskLibrary.Id);
            visitorsLibrary.Add(BeaudenNielsen_ObeliskLibrary);
            var KellanConroy_AeosLibrary = new VisitorLibrary(KellanConroy.Id, AeosLibrary.Id);
            visitorsLibrary.Add(KellanConroy_AeosLibrary);
            var KellanConroy_ObeliskLibrary = new VisitorLibrary(KellanConroy.Id, ObeliskLibrary.Id);
            visitorsLibrary.Add(KellanConroy_ObeliskLibrary);


            return (visitorsLibrary, visitors, libraries, districts, Chester);
        }
    }

    class VisitorLibrary
    {
        public Guid LibraryId { get; }

        public Guid VisitorId { get; }

        public VisitorLibrary(Guid visitorId, Guid libraryId)
        {
            VisitorId = visitorId;
            LibraryId = libraryId;
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

//Придумай еще как минимум 5 запросов с выводом какой-либо статистики. А лучше больше. Модель довольно богатая, можно хорошо на ней попрактиковаться.

//Если хочешь еще усложнить – создай города. И добавится еще один уровень для группировок и фильтраций.
