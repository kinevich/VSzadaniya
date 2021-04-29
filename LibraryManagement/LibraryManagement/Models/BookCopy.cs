using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookCopy
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Book")]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [DisplayName("Edition")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        public virtual Edition Edition { get; set; }

        [DisplayName("Release form")]
        public int ReleaseFormId { get; set; }

        [ForeignKey("ReleaseFormId")]
        public virtual ReleaseForm ReleaseForm { get; set; }


    }
}
