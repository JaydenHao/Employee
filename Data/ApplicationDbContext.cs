using EmployeeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeApplication.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(180);
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
