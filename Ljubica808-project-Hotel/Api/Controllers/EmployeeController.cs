using Api.Common;
using Application.Common;
using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return this.HandleResult(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            return this.HandleResult(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] PostEmployeeDTO employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return this.HandleResult(result);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] PutEmployeeDTO employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            return this.HandleResult(result);
        }
    }
}
