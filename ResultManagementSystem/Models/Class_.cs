using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class Class_
    {
        [Key]
        public int ClassID { get; set; }

        [Display(Name = "Class")]
        public string Name { get; set; }
        public ICollection<ClassInfo> ClassInfos { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
