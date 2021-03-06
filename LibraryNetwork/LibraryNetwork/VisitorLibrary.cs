﻿using System;

namespace LibraryNetwork
{
    class VisitorLibrary
    {
        public Guid Id { get; }

        public Guid LibraryId { get; }

        public Guid VisitorId { get; }

        public VisitorLibrary(Guid visitorId, Guid libraryId)
        {
            Id = Guid.NewGuid();
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