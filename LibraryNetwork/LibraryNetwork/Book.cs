using System;

namespace LibraryNetwork
{
    class Book
    {
        public Guid Id { get; }

        public Guid AuthorId { get; }

        public Guid GenreId { get; }

        public string Title { get; }

        public Book(string title, Guid authorId, Guid genreId)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId;
            GenreId = genreId;
            Title = title;
        }
    }
}
