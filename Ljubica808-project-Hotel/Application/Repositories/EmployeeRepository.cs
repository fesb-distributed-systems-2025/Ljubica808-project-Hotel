using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(
            IApplicationDbContext dbContext,
            ILogger<EmployeeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                _logger.LogWarning("Employee with id {EmployeeId} not found.", id);
                throw new KeyNotFoundException($"Employee {id} not found.");
            }

            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var oldEmployee = await _dbContext.Employees.FindAsync(employee.Id);

            if (oldEmployee == null)
            {
                _logger.LogWarning("Employee with id {EmployeeId} not found for update.", employee.Id);
                throw new KeyNotFoundException($"Employee {employee.Id} not found.");
            }

            oldEmployee.FirstName = employee.FirstName;
            oldEmployee.LastName = employee.LastName;
            oldEmployee.Position = employee.Position;
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                _logger.LogWarning("Employee with id {EmployeeId} not found for delete.", id);
                throw new KeyNotFoundException($"Employee {id} not found.");
            }

            _dbContext.Employees.Remove(employee);
        }
    }
}
