using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Models.AccountViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }

        public List<SelectListItem> Roles { get; set; }
        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }
}
