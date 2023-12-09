using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? Age { get; set; }

       // public int Salary { get; set; }
        [ForeignKey(nameof(EmployeeSalary))]
        public int EmployeeSalaryId {  get; set; }
        public virtual EmployeeSalary? employeeSalary { get; set; }  
    }
}
