using System.ComponentModel.DataAnnotations;

namespace EFCore.Model
{
    public class EmployeeSalary
    {
        [Key]
        public int Id { get; set; }
        public int? Salary { get; set; }
    }
}
