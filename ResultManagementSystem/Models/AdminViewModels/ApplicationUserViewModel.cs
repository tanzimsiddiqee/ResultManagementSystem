using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models.AdminViewModels
{
    public class ApplicationUserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<ApplicationUser> Managers { get; set; }
        public IEnumerable<ApplicationUser> Administrators { get; set; }
    }
}
