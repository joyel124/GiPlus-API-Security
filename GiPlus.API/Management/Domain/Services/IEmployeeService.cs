using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> ListAsync();
    Task<IEnumerable<Employee>> ListByUserIdAsync(int userId);
    Task<EmployeeResponse> SaveAsync(Employee employee);
    Task<EmployeeResponse> UpdateAsync(int employeeId, Employee employee);
    Task<EmployeeResponse> DeleteAsync(int employeeId);
}