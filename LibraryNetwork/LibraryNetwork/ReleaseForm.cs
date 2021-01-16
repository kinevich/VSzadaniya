using System;

namespace LibraryNetwork
{
    class ReleaseForm
    {
        public Guid Id { get; }

        public string Name { get; }

        public ReleaseForm(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
