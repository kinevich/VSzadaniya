using System;

namespace LibraryNetwork
{
    class Library
    {
        public Guid Id { get; }

        public Guid DistrictId { get; }

        public string Name { get; }

        public TimeSpan Limit { get; }
        
        public Library(string name, Guid districtId, TimeSpan limit)
        {
            Id = Guid.NewGuid();
            DistrictId = districtId;
            Name = name;
            Limit = limit;
        }
    }
}
