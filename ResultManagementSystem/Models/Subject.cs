using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class Subject
    {
        [Key]
        public int SubjectCode { get; set; }

        public string Name { get; set; }

    }
}
