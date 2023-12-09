using EFCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class ApplicationDbContext:DbContext
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
           
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
