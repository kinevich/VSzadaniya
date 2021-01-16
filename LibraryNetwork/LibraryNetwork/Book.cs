using System;

namespace LibraryNetwork
{
    class Book
    {
        public Guid Id { get; }

        public Guid AuthorId { get; }

        public Guid GenreId { get; }

        public string Title { get; }

        public Book(string title, Author author, Genre genre)
        {
            Id = Guid.NewGuid();
            AuthorId = author.Id;
            GenreId = genre.Id;
            Title = title;
        }
    }
}
