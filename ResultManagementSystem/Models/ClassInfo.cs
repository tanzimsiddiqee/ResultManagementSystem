using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class ClassInfo
    {
        [Key]
        public int ClassInfoID { get; set; }

        [Display(Name ="Class Info")]
        public string ClassInfoName { get; set; }

        public int ClassID { get; set; }
        public Class_ Class_ { get; set; }
        public int SectionID { get; set; }
        public Section Section { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Subject_Teacher> Subject_Teachers { get; set; }

    }
}
