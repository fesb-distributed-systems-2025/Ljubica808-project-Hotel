using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Koristimo fake database čisto da provjerimo je radi,
        // Implementirat ćemo svoju na sljedećim vježbama
        // Fake database (in-memory storage) 
        private static List<Employee> _employees = new();
        private static int _idCounter = 1;

        public EmployeeController() { }

        [HttpGet("employees")]
        public IEnumerable<Employee> GetAllEmployees() => _employees;

        [HttpGet("employees/{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            // LINQ(Language Integrated Query)
            //FirstorDefault-pretražuje listu od početka i vraća prvi element koji zadovoljava uvjet
            var emp = _employees.FirstOrDefault(x => x.Id == id); // Lamba izraz
            return emp == null ? NotFound("Employee not found") : Ok(emp);
        }

        [HttpPost("new")]
        public ActionResult<int> AddEmployee([FromBody] PostEmployeeDTO dto)
        {
            var emp = new Employee
            {
                Id = _idCounter++,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Position = dto.Position
            };

            _employees.Add(emp);
            return Ok(emp.Id);
        }

        [HttpPut("edit")]
        public ActionResult<int> EditEmployee([FromBody] Employee updated)
        {
            var emp = _employees.FirstOrDefault(x => x.Id == updated.Id);

            if (emp == null) return NotFound("Employee not found");

            emp.FirstName = updated.FirstName;
            emp.LastName = updated.LastName;
            emp.Position = updated.Position;

            return Ok(emp.Id);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<int> DeleteEmployee(int id)
        {
            var exists = _employees.Any(x => x.Id == id);
            if (!exists) return NotFound("Employee not found");

            _employees = _employees.Where(x => x.Id != id).ToList();
            return Ok(id);
        }
    }
}
