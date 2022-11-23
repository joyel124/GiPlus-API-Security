using GiPlus.API.Management.Domain.Models;

namespace GiPlus.API.Management.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> ListAsync();
    Task AddSync(Employee employee);
    Task<Employee> FindByIdAsync(int employeeId);
    Task<Employee> FindByNameAsync(string name);
    Task<IEnumerable<Employee>> FindByUserIdAsync(int userId);
    void Update(Employee employee);
    void Remove(Employee employee);
}