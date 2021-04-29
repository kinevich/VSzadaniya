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
        [Key]
        public int Id { get; set; }

        [DisplayName("Library")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Limit days")]
        [Required]
        public int LimitDays { get; set; }

        [DisplayName("District")]
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
}
