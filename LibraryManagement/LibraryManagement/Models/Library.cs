using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Library
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LimitDays { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }
    }
}
