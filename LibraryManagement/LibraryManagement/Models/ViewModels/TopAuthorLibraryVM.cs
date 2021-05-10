using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class TopAuthorLibraryVM
    {
        public Author TopAuthor { get; set; }
        
        public Library Library { get; set; }
    }
}
