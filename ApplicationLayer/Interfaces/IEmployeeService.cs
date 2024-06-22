using DDD.APP.ApplicationLayer.Dto;

namespace DDD.APP.ApplicationLayer.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto> GetByIdAsync(int id);
    Task AddAsync(EmployeeDto employeeDto);
    Task UpdateAsync(EmployeeDto employeeDto);
    Task DeleteAsync(int id);
}