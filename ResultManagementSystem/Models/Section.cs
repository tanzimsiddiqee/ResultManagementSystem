using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class Section
    {
        [Key]
        public int SectionID { get; set; }

        [Display(Name = "Section")]
        public string Name { get; set; }
    }
}
