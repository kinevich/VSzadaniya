using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryNetwork
{
    class City
    {
        public Guid Id { get; }

        public string Name { get; }

        public City(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
