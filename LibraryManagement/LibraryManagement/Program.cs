using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
//Система управления бибилиотеками
//Должна иметь следующие функции:

//-Управление районами города(управление = создание / отобржаение(списка + одной записи) / обновление / удаление CRUD)
//-библиотеками(отображаются сгруппированными по районам)
//- жанрами книги
// - авторами
// - книгами
// - изданиями
// (Посетителей можешь не делать, если будешь чувствовать, что материал усвоен и можешь делать такие страницы)

//Нужно сделать отдельную вкладку "Статистика" и на этой вкладке виды статистики будут обычным списком ссылок. При переходе по ссылке должна отображаться выбранная статистика.

//Отображать статистику по:

//-Количество библиотек в каждом районе
//-Сколько книг в каждом жанре
//-Сколько авторов пишет для каждого жанра
//-Самый "плодовитый" (написавший максимальное количество книг) автор по каждой библиотеке
