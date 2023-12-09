using EFCore.Data;
using EFCore.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using static EFCore.Repository.EmployeeRepository;

namespace EFCore.Repository
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext dbcon;

        public EmployeeRepository(ApplicationDbContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public List<Employee> GetAllEmployees()
        {
            return dbcon.Employees.Include(s => s.employeeSalary).ToList();
        }

        public List<Employee> SearchEmployees(string searchName)
        {
            return dbcon.Employees.Where(s => s.Name.Contains(searchName)).Include(s => s.employeeSalary).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return dbcon.Employees.Include(s => s.employeeSalary).FirstOrDefault(s => s.Id == id);
        }

        public Employee CreateEmployee(Employee employee)
        {
            dbcon.Add(employee);
            dbcon.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(int id, Employee updatedEmployee)
        {
            var existingEmployee = dbcon.Employees.Include(s => s.employeeSalary).FirstOrDefault(s => s.Id == id);

            if (existingEmployee == null)  return null;

            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Age = updatedEmployee.Age;
            existingEmployee.employeeSalary.Salary = updatedEmployee.employeeSalary.Salary;
            dbcon.SaveChanges();
            return existingEmployee;
        }

        public void Patch(int id, Employee employee)
        {
            var existingEmployee = dbcon.Employees.Include(s => s.employeeSalary).FirstOrDefault(s => s.Id == id);

            if (existingEmployee.Name != employee.Name && employee.Name != null)  existingEmployee.Name = employee.Name;    

            if (existingEmployee.Age != employee.Age && employee.Age!= null)  existingEmployee.Age = employee.Age;

            dbcon.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = dbcon.Employees.Include(s => s.employeeSalary).FirstOrDefault(s => s.Id == id);

            if (employee == null)  return false;
        

            if (employee.employeeSalary != null)   dbcon.EmployeeSalaries.Remove(employee.employeeSalary);

            dbcon.Employees.Remove(employee);
            dbcon.SaveChanges();

            return true;
        }

        public Dictionary<int?, List<Employee>> GroupByAge()
        {
            return dbcon.Employees
                .AsEnumerable()
                .GroupBy(employee => employee.Age)
                .ToDictionary(
                    x => x.Key,
                    x => x.ToList()
                );
        }

    }
    }


