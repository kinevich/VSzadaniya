using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class DistrictLibrariesVM
    {
        public District District { get; set; }

        public IEnumerable<Library> Libraries { get; set; }
    }
}
