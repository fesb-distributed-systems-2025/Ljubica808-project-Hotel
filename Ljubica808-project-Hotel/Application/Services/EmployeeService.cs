using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee?>> GetAllEmployees() => await _employeeRepository.GetEmployees();
        public async Task<Employee?> GetEmployeeById(int id) => await _employeeRepository.GetEmployeeById(id);

        public async Task<string> AddEmployee(PostEmployeeDTO dto)
        {
            var employee = dto.ToModel();
            var validationResult = ValidateEmployee(employee);
            if (validationResult != null)
                return validationResult;

            _employeeRepository.CreateEmployee(employee);
            await _unitOfWork.SaveChangesAsync();

            return $"Employee successfully created with Id: {employee.Id}";
        }

        public async Task<string> UpdateEmployee(PutEmployeeDTO dto)
        {
            var employee = dto.ToModel();
            var validationResult = ValidateEmployee(employee);
            if (validationResult != null)
                return validationResult;

            await _employeeRepository.UpdateEmployee(employee);
            await _unitOfWork.SaveChangesAsync();

            return $"Employee successfully updated with Id: {employee.Id}";
        }

        public async Task<string> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
            await _unitOfWork.SaveChangesAsync();

            return $"Employee successfully deleted with Id: {id}";
        }

        private string? ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FirstName))
                return "First name is required.";

            if (string.IsNullOrWhiteSpace(employee.LastName))
                return "Last name is required.";

            return null;
        }
    }
}
