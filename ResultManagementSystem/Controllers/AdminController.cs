using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultManagementSystem.Models;
using ResultManagementSystem.Models.AccountViewModels;
using ResultManagementSystem.Models.AdminViewModels;

namespace ResultManagementSystem.Controllers
{
    [Authorize(Roles = "Administraion")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Application User (Creat, Details, Update, Delete)

        public async Task<IActionResult> AppUser()
        {
            var admins = await userManager.GetUsersInRoleAsync("Administrator");
            var managers = await userManager.GetUsersInRoleAsync("Manager");
            var users = await userManager.GetUsersInRoleAsync("User");
            var appUser = new ApplicationUserViewModel()
            {
                Administrators = admins,
                Managers = managers,
                Users = users
            };
            return View(appUser);
        }

        public IActionResult AddUser()
        {
            var model = new UserViewModel();
            model.Roles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    DOB = model.DOB,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = model.IsEmailConfirmed,
                    PhoneNumber = model.Phone,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var applicationRole = await roleManager.FindByIdAsync(model.RoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("AppUser");
                        }
                    }
                }

            }
            return View(model);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            UserViewModel model = new UserViewModel();
            model.Roles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Name = user.Name;
                    model.DOB = user.DOB;
                    model.UserName = user.UserName;
                    model.Email = user.Email;
                    model.IsEmailConfirmed = user.EmailConfirmed;
                    model.Phone = user.PhoneNumber;
                    model.RoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.DOB = model.DOB;
                    user.UserName = model.UserName;
                    user.UserName = model.Email;
                    user.EmailConfirmed = model.IsEmailConfirmed;
                    user.PhoneNumber = model.Phone;
                    var existingRole = userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    var modelRole = await roleManager.FindByIdAsync(model.RoleId);
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {

                        var roleResult = await userManager.RemoveFromRoleAsync(user, existingRole);
                        var record = userManager.GetRolesAsync(user);
                        if (roleResult.Succeeded && record != null)
                        {
                            var Result = await userManager.AddToRoleAsync(user, modelRole.Name);
                            return RedirectToAction("AppUser");
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AppUser");
                    }
                }
            }
            return RedirectToAction("AppUser");
        }
        #endregion
    }
}