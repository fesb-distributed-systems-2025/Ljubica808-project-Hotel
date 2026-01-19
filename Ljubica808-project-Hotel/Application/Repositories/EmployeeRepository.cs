using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public EmployeeRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                throw new KeyNotFoundException($"Employee {id} not found.");

            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var oldEmployee = await _dbContext.Employees.FindAsync(employee.Id)
                ?? throw new KeyNotFoundException($"Employee {employee.Id} not found.");

            oldEmployee.FirstName = employee.FirstName;
            oldEmployee.LastName = employee.LastName;
            oldEmployee.Position = employee.Position;
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
                throw new KeyNotFoundException($"Employee {id} not found.");

            _dbContext.Employees.Remove(employee);
        }
    }
}
