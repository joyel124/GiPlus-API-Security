using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Management.Persistence.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Employee>> ListAsync()
    {
        return await _context.Employees
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddSync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task<Employee> FindByIdAsync(int employeeId)
    {
        return await _context.Employees
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == employeeId);
    }

    public async Task<Employee> FindByNameAsync(string name)
    {
        return await _context.Employees
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.FirstName == name);
    }

    public async Task<IEnumerable<Employee>> FindByUserIdAsync(int userId)
    {
        return await _context.Employees
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public void Remove(Employee employee)
    {
        _context.Employees.Remove(employee);
    }
}