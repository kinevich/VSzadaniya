using System;

namespace LibraryNetwork
{
    class Edition
    {
        public Guid Id { get; }

        public int Number { get; }

        public int Pages { get; }

        public Edition(int number, int pages)
        {
            Id = Guid.NewGuid();
            Number = number;
            Pages = pages;
        }
    }
}
