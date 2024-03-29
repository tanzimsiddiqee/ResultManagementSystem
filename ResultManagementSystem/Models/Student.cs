﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string StdName { get; set; }
        public int? ClassInfoID { get; set; }
        public ClassInfo ClassInfo { get; set; }
    }
}
