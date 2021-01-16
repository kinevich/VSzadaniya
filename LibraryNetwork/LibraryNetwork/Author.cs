using System;

namespace LibraryNetwork
{
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
