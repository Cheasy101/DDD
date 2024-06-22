using DDD.APP.ApplicationLayer.Interfaces;
using DDD.APP.Domain_Layer.Entities;
using DDD.APP.Domain_Layer.Interfaces;
using DDD.APP.Exceptions;
using DDD.APP.ApplicationLayer.Dto;

namespace DDD.APP.ApplicationLayer.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await employeeRepository.GetAllAsync();
        return employees.Select(e => new EmployeeDto { Id = e.Id, FullName = e.FullName, Position = e.Position });
    }

    public async Task<EmployeeDto> GetByIdAsync(int id)
    {
        var employee = await employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new EmployeeNotFoundException();
        }
        return new EmployeeDto { Id = employee.Id, FullName = employee.FullName, Position = employee.Position };
    }

    public async Task AddAsync(EmployeeDto employeeDto)
    {
        var existingEmployee = await employeeRepository.GetByFullNameAsync(employeeDto.FullName);
        if (existingEmployee != null)
        {
            throw new EmployeeAlreadyExistsException();
        }

        var employee = new Employee { FullName = employeeDto.FullName, Position = employeeDto.Position };
        await employeeRepository.AddAsync(employee);
    }

    public async Task UpdateAsync(EmployeeDto employeeDto)
    {
        var existingEmployee = await employeeRepository.GetByFullNameAsync(employeeDto.FullName);
        if (existingEmployee != null && existingEmployee.Id != employeeDto.Id)
        {
            throw new EmployeeAlreadyExistsException();
        }

        var employee = new Employee
        {
            Id = employeeDto.Id,
            FullName = employeeDto.FullName,
            Position = employeeDto.Position
        };
        await employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteAsync(int id)
    {
        await employeeRepository.DeleteAsync(id);
    }
}