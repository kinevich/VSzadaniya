using System;

namespace LibraryNetwork
{
    class Edition
    {
        public Guid Id { get; }

        public string Number { get; }

        public int Pages { get; }

        public Edition(string number, int pages)
        {
            Id = Guid.NewGuid();
            Number = number;
            Pages = pages;
        }
    }
}
