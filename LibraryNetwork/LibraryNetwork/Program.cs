using System;

namespace LibraryNetwork
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class District
    {

    }

    class Library
    {

    }

    class Book
    {
        public Guid ID { get; }

        public string Title { get; }

        public string Author { get; }

        public string Genre { get; }

        public Book(string title, string author, string genre)
        {
            ID = Guid.NewGuid();
            Title = title;
            Author = author;
            Genre = genre;
        }
    }

    class BookCopy
    {
        public Guid ID { get; }

        public Book Book { get; }

        public Edition Edition { get; }

        public string ReleaseForm { get; }

        public BookCopy(Book book, Edition edition, string releaseForm)
        {
            ID = Guid.NewGuid();
            Book = book;
            Edition = edition;
            ReleaseForm = releaseForm;
        }

    }

    class Edition
    {
        public Guid ID { get; }

        public int Number { get; }

        public int Pages { get; }

        public Edition(int number, int pages)
        {
            ID = Guid.NewGuid();
            Number = number;
            Pages = pages;
        }
    }


}
