using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookCopyLibrary
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("BookCopy")]
        public int BookCopyId { get; set; }

        [ForeignKey("BookCopyId")]
        public virtual BookCopy BookCopy { get; set; }

        [DisplayName("Library")]
        public int LibraryId { get; set; }

        [ForeignKey("LibraryId")]
        public virtual Library Library { get; set; }
    }
}
