using BackendAPI.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly BackendAPIDbContext _dbContext;
        public EmployeesController(BackendAPIDbContext backendAPIDbContext) 
        {
            _dbContext = backendAPIDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeList = await _dbContext.Employees.ToListAsync();
            return Ok(employeeList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployeeByID([FromRoute] Guid id)
        {
           var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null) { return NotFound(); }
            else   return Ok(employee);    
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _dbContext.Employees.AddAsync(employeeRequest);
            await _dbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) return NotFound();

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _dbContext.SaveChangesAsync();
            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null) return NotFound();    

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return Ok(employee);

        }
    }
}
