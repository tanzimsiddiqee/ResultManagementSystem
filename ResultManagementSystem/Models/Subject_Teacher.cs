using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class Subject_Teacher
    {
        [Key]
        public int ID { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public int ClassID { get; set; }
        public Class_ Class_ { get; set; }
        public int SubjectCode { get; set; }
        public Subject Subject { get; set; }


    }
}
