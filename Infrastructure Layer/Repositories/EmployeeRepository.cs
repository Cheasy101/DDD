using DDD.APP.Domain_Layer.Entities;
using DDD.APP.Domain_Layer.Interfaces;
using DDD.APP.Infrastructure_Layer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DDD.APP.Infrastructure_Layer.Repositories
{
    public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return (await context.Employees.FindAsync(id))!;
        }

        public async Task<Employee> GetByFullNameAsync(string fullName)
        {
            return (await context.Employees.SingleOrDefaultAsync(e => e.FullName == fullName))!;
        }

        public async Task AddAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
        }
    }
}