using Application.Common;
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

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Employee?>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            return Result<IEnumerable<Employee?>>.Success(employees);
        }

        public async Task<Result<Employee?>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return Result<Employee?>.Success(employee);
        }

        public async Task<Result<object>> AddEmployee(PostEmployeeDTO dto)
        {
            var employee = dto.ToModel();

            var validationResult = ValidateEmployee(employee);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            _employeeRepository.CreateEmployee(employee);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateEmployee(PutEmployeeDTO dto)
        {
            var employee = dto.ToModel();

            var validationResult = ValidateEmployee(employee);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            await _employeeRepository.UpdateEmployee(employee);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        // ===== VALIDATION (HW-06 STYLE) =====
        private ValidationResult ValidateEmployee(Employee employee)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(employee.FirstName))
                result.ValidationItems.Add("First name is required.");
            else if (employee.FirstName.Length > Employee.FirstNameMaxLength)
                result.ValidationItems.Add(
                    $"First name cannot exceed {Employee.FirstNameMaxLength} characters.");

            if (string.IsNullOrWhiteSpace(employee.LastName))
                result.ValidationItems.Add("Last name is required.");
            else if (employee.LastName.Length > Employee.LastNameMaxLength)
                result.ValidationItems.Add(
                    $"Last name cannot exceed {Employee.LastNameMaxLength} characters.");

            if (!Enum.IsDefined(typeof(EmployeePosition), employee.Position))
                result.ValidationItems.Add("Position is required.");

            return result;
        }
    }
}
