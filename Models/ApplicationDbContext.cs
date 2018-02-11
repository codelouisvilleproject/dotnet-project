using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetproject.Models;

namespace dotnet_project.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Models
        public DbSet<Activity> Activities { get; set; }   
        public DbSet<CodeLouisvilleStudent> CodeLouisvileStudents { get; set; }
    }
}
