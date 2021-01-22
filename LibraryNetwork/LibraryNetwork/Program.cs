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
             List<District> districts, City city, List<Book> books, List<Genre> genres) = GetData();

            // 16 libraries, 5 districts

            //ListLibraryNames(libraries);
            //ListGroupedLibrariesByDistricts(libraries, districts);
            //ListDistrictsDontHaveLibraries(libraries, districts);
            //ListAllVisitors(libraries, visitorsLibrary, visitors);
            //ListVisitorsByLibraries(libraries, visitors, visitorsLibrary);
            //ListVisitorsByDistricts(libraries, districts, visitors, visitorsLibrary);
            //ListVisitorsCountByDistricts(libraries, districts, visitorsLibrary);
            ListBooksCountByGenres(books, genres);
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
            var visitorsByLibraries = from library in libraries
                        join visitorLibrary in visitorsLibrary on library.Id equals visitorLibrary.LibraryId
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



        private static (List<VisitorLibrary> visitorsLibrary, List<Visitor> visitors,
                        List<Library> libraries, List<District> districts, City city,
                        List<Book> books, List<Genre> genres) GetData()

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

            var genres = new List<Genre>();

            var crimeGenre = new Genre("Crime Genre"); // 8
            genres.Add(crimeGenre);
            var fantasyGenre = new Genre("Fantasy Genre"); // 7
            genres.Add(fantasyGenre);
            var mysteryGenre = new Genre("Mystery Genre"); // 11
            genres.Add(mysteryGenre);
            var romanceGenre = new Genre("Romance Genre"); // 9 
            genres.Add(romanceGenre);
            var sciFiGenre = new Genre("Sci-Fi Genre"); // 5
            genres.Add(sciFiGenre);

            var authors = new List<Author>();

            var KitStrickland = new Author("Kit Strickland");
            authors.Add(KitStrickland);
            var MikeWheeler = new Author("Mike Wheeler");
            authors.Add(MikeWheeler);
            var FredGriffith = new Author("Fred Griffith");
            authors.Add(FredGriffith);
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
            var SpikeWatts = new Author("Spike Watts");
            authors.Add(SpikeWatts);
            var JackKain = new Author("Jack Kain");
            authors.Add(JackKain);
            var LeroyWatkins = new Author("Leroy Watkins");
            authors.Add(LeroyWatkins);
            var BradStrickland = new Author("Brad Strickland");
            authors.Add(BradStrickland);
            var RockyReid = new Author("Rocky Reid");
            authors.Add(RockyReid);
            var BlakeHoyles = new Author("Blake Hoyles");
            authors.Add(BlakeHoyles);
            var DwayneCurrey = new Author("Dwayne Currey");
            authors.Add(DwayneCurrey);
            var FordBarker = new Author("Ford Barker");
            authors.Add(FordBarker);
            var TobyBennett = new Author("Toby Bennett");
            authors.Add(TobyBennett);
            var WallyBlake = new Author("Wally Blake");
            authors.Add(WallyBlake);
            var DamianYoung = new Author("Damian Young");
            authors.Add(DamianYoung);
            var LaurenceHudson = new Author("Laurence Hudson");
            authors.Add(LaurenceHudson);
            var MortonFoster = new Author("Morton Foster");
            authors.Add(MortonFoster);
            var GlenFreeman = new Author("Glen Freeman");
            authors.Add(GlenFreeman);
            var TommyParham = new Author("Tommy Parham");
            authors.Add(TommyParham);
            var WintonStone = new Author("Winton Stone");
            authors.Add(WintonStone);
            var ErnestHodgson = new Author("Ernest Hodgson");
            authors.Add(ErnestHodgson);
            var WalterAndrews = new Author("Walter Andrews");
            authors.Add(WalterAndrews);
            var BertLamb = new Author("Bert Lamb");
            authors.Add(BertLamb);
            var BarrettMoss = new Author("Barrett Moss");
            authors.Add(BarrettMoss);
            var MaynardArnold = new Author("Maynard Arnold");
            authors.Add(MaynardArnold);
            var FrankSandoval = new Author("Frank Sandoval");
            authors.Add(FrankSandoval);
            var CliffordRehbein = new Author("Clifford Rehbein");
            authors.Add(CliffordRehbein);
            var KitStevenson = new Author("Kit Stevenson");
            authors.Add(KitStevenson);
            var SimonWalton = new Author("Simon Walton");
            authors.Add(SimonWalton);
            var KeithTurner = new Author("Keith Turner");
            authors.Add(KeithTurner);
            var OllieSkinner = new Author("Ollie Skinner");
            authors.Add(OllieSkinner);
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

            var TheVault = new Book(@"""The Vault""", KitStrickland.Id, crimeGenre.Id);
            books.Add(TheVault);
            var GhostRiders = new Book(@"""Ghost Riders""", MikeWheeler.Id, crimeGenre.Id);
            books.Add(GhostRiders);
            var Hypothermia = new Book(@"""Hypothermia""", FredGriffith.Id, crimeGenre.Id);
            books.Add(Hypothermia);
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
            var TheStone = new Book(@"""The Stone""", SpikeWatts.Id, fantasyGenre.Id);
            books.Add(TheStone);
            var TheMaster = new Book(@"""The Master""", JackKain.Id, fantasyGenre.Id);
            books.Add(TheMaster);
            var TheLast = new Book(@"""The Last""", LeroyWatkins.Id, fantasyGenre.Id);
            books.Add(TheLast);
            var AWizard = new Book(@"""A Wizard""", BradStrickland.Id, fantasyGenre.Id);
            books.Add(AWizard);
            var TheMystery = new Book(@"""The Mystery""", RockyReid.Id, mysteryGenre.Id);
            books.Add(TheMystery);
            var TheZone = new Book(@"""The Zone""", BlakeHoyles.Id, mysteryGenre.Id);
            books.Add(TheZone);
            var Dead = new Book(@"""Dead""", DwayneCurrey.Id, mysteryGenre.Id);
            books.Add(Dead);
            var Creep = new Book(@"""Creep""", FordBarker.Id, mysteryGenre.Id);
            books.Add(Creep);
            var Dark = new Book(@"""Dark""", TobyBennett.Id, mysteryGenre.Id);
            books.Add(Dark);
            var Magic = new Book(@"""Magic""", WallyBlake.Id, mysteryGenre.Id);
            books.Add(Magic);
            var TheDevil = new Book(@"""The Devil""", DamianYoung.Id, mysteryGenre.Id);
            books.Add(TheDevil);
            var Mysteries = new Book(@"""Mysteries""", LaurenceHudson.Id, mysteryGenre.Id);
            books.Add(Mysteries);
            var TheQueer = new Book(@"""The Queer""", MortonFoster.Id, mysteryGenre.Id);
            books.Add(TheQueer);
            var ThePortrait = new Book(@"""The Portrait""", GlenFreeman.Id, mysteryGenre.Id);
            books.Add(ThePortrait);
            var TheSecret = new Book(@"""The Secret""", TommyParham.Id, mysteryGenre.Id);
            books.Add(TheSecret);
            var LordOfScoundrels = new Book(@"""Lord of Scoundrels""", WintonStone.Id, romanceGenre.Id);
            books.Add(LordOfScoundrels);
            var Indigo = new Book(@"""Indigo""", ErnestHodgson.Id, romanceGenre.Id);
            books.Add(Indigo);
            var Casablanca = new Book(@"""Casablanca""", WalterAndrews.Id, romanceGenre.Id);
            books.Add(Casablanca);
            var ANightAtTheOpera = new Book(@"""A Night at the Opera""", BertLamb.Id, romanceGenre.Id);
            books.Add(ANightAtTheOpera);
            var CallMe = new Book(@"""Call Me""", BarrettMoss.Id, romanceGenre.Id);
            books.Add(CallMe);
            var TopHat = new Book(@"""Top Hat""", MaynardArnold.Id, romanceGenre.Id);
            books.Add(TopHat);
            var Her = new Book(@"""Her""", FrankSandoval.Id, romanceGenre.Id);
            books.Add(Her);
            var TheAfricanQueen = new Book(@"""The African Queen""", CliffordRehbein.Id, romanceGenre.Id);
            books.Add(TheAfricanQueen);
            var TheLostWeekend = new Book(@"""The Lost Weekend""", KitStevenson.Id, romanceGenre.Id);
            books.Add(TheLostWeekend);
            var WhatIf = new Book(@"""What If""", SimonWalton.Id, sciFiGenre.Id);
            books.Add(WhatIf);
            var BlackHole = new Book(@"""Black Hole""", KeithTurner.Id, sciFiGenre.Id);
            books.Add(BlackHole);
            var Sciencia = new Book(@"""Sciencia""", OllieSkinner.Id, sciFiGenre.Id);
            books.Add(Sciencia);
            var AtomSmashing = new Book(@"""Atom Smashing""", RyanLynch.Id, sciFiGenre.Id);
            books.Add(AtomSmashing);
            var FoodChemistry = new Book(@"""Food Chemistry""", DentonFrazier.Id, sciFiGenre.Id);
            books.Add(FoodChemistry);



            return (visitorsLibrary, visitors, libraries, districts, Chester, books, genres);
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
