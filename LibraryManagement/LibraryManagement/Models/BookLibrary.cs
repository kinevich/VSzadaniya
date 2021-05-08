using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookLibrary
    {
        public Guid Id { get; set; }

        [DisplayName("Library")]
        public Guid LibraryId { get; set; }

        public Library Library { get; set; }

        [DisplayName("Book")]
        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
