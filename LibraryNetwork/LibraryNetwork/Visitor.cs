using System;

namespace LibraryNetwork
{
    class Visitor
    {
        public Guid Id { get; }

        public string Name { get; } 

        public Visitor(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
