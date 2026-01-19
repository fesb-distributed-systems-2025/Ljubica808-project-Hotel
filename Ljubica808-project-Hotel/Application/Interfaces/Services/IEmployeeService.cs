using Application.Common;
using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Result<IEnumerable<Employee?>>> GetAllEmployees();
        Task<Result<Employee?>> GetEmployeeById(int id);
        Task<Result<object>> AddEmployee(PostEmployeeDTO employee);
        Task<Result<object>> UpdateEmployee(PutEmployeeDTO employee);
        Task<Result<object>> DeleteEmployee(int id);
    }
}
