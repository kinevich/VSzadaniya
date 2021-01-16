using System;

namespace LibraryNetwork
{
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
}
