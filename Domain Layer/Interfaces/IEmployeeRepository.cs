using DDD.APP.Domain_Layer.Entities;

namespace DDD.APP.Domain_Layer.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int id);
    Task<Employee> GetByFullNameAsync(string fullName);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
}