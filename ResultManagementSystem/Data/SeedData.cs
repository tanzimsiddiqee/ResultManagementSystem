using Microsoft.AspNetCore.Identity;
using ResultManagementSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string AdminPw)
        {
            // For sample purposes seed both with the same password.
            // Password is set with the following:
            // dotnet user-secrets set SeedUserPW <pw>
            // The admin user can do anything

            var adminID = await EnsureUser(serviceProvider, AdminPw, "admin@smss.com");
            await EnsureRole(serviceProvider, adminID, "Administrator", "Teacher", "User");

        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string AdminPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = UserName };
                await userManager.CreateAsync(user, AdminPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role, string role2, string role3)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role3))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role3));
                if (!await roleManager.RoleExistsAsync(role2))
                {
                    IR = await roleManager.CreateAsync(new IdentityRole(role2));
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        IR = await roleManager.CreateAsync(new IdentityRole(role));

                    }
                }
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
