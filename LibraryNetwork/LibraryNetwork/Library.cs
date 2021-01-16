using System;

namespace LibraryNetwork
{
    class Library
    {
        public Guid Id { get; }

        public Guid DistrictId { get; }

        public string Name { get; }
        
        public Library(string name, Guid districtId)
        {
            Id = Guid.NewGuid();
            DistrictId = districtId;
            Name = name;
        }
    }
}
