using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Edition
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Edition")]
        [Required]
        public string EditionNumber { get; set; }

        [DisplayName("Amount of pages")]
        [Required]
        public int PagesAmount { get; set; }

        [DisplayName("Book")]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}
