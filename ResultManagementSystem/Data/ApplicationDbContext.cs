using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResultManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class_> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ClassInfo> ClassInfo { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Subject_Teacher> Subject_Teachers { get; set; }
    }
}
