using EFCore.Data;
using EFCore.Model;
using EFCore.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            var employees = employeeRepository.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("search")]
        public IActionResult Search(string searchTerm)
        {
            var matchingEmployees = employeeRepository.SearchEmployees(searchTerm);
            return Ok(matchingEmployees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = employeeRepository.GetEmployeeById(id);
            if (employee == null)  return NotFound();
            return Ok(employee);
        }


        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var createdEmployee = employeeRepository.CreateEmployee(employee);
            return Ok(createdEmployee);
        }


        [HttpPut("{id}")]
        public ActionResult<Employee> Update(int id, [FromBody] Employee updatedEmployee)
        {
            var updated = employeeRepository.UpdateEmployee(id, updatedEmployee);
            if (updated == null)  return NotFound();
            return Ok(updated);
        }


        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody] Employee employee)
        {

            employeeRepository.Patch(id, employee);
            return Ok(employee);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleted = employeeRepository.DeleteEmployee(id);
            if (!deleted)  return NotFound();
            return NoContent();
        }
        [HttpGet("groupbydepartment")]

        public ActionResult GroupByEmployeesAge()
        {
            var groupedEmployees = employeeRepository.GroupByAge();
            return Ok(groupedEmployees);
        }
    }

}