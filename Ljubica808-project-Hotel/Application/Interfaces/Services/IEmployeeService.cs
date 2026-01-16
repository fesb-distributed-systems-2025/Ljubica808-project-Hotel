using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee?>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<string> AddEmployee(PostEmployeeDTO employee);
        Task<string> UpdateEmployee(PutEmployeeDTO employee);
        Task<string> DeleteEmployee(int id);
    }
}
