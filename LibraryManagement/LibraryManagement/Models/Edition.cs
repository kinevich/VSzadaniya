﻿using System;
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
        public Guid Id { get; set; }

        public int EditionNumber { get; set; }

        public int PagesAmount { get; set; }

        [DisplayName("Book")]
        public Guid BookId { get; set; }

        public  Book Book { get; set; }
    }
}
