using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DisplayName("Genre")]
        public Guid GenreId { get; set; }

        public Genre Genre { get; set; }

        [DisplayName("Author")]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
