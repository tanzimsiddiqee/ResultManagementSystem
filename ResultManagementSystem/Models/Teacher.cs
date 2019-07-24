using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Teacher ID")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        public ICollection<Subject_Teacher> Subject_Teachers { get; set; }
    }
}
