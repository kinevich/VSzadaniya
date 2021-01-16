using System;
using System.Collections.Generic;

namespace LibraryNetwork
{
    class District
    {
        public Guid Id { get; }

        public Guid CityId { get; }

        public string Name { get; }

        public District(string name, Guid cityId)
        {
            Id = Guid.NewGuid();
            CityId = cityId;
            Name = name;
        }
    }
}
