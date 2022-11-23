using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Repositories;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Domain.Services.Communication;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Shared.Domain.Repositories;

namespace GiPlus.API.Management.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Employee>> ListAsync()
    {
        return await _employeeRepository.ListAsync();
    }

    public async Task<IEnumerable<Employee>> ListByUserIdAsync(int userId)
    {
        return await _employeeRepository.FindByUserIdAsync(userId);
    }

    public async Task<EmployeeResponse> SaveAsync(Employee employee)
    {
        //Validate UserId
        var existingUser = await _userRepository.FindByIdAsync(employee.UserId);
        if (existingUser == null)
            return new EmployeeResponse("Invalid User");
        try
        {
            //Add Employee
            await _employeeRepository.AddSync(employee);
            //Complete transaction
            await _unitOfWork.CompleteAsync();
            //Return Response
            return new EmployeeResponse(employee);
        }
        catch (Exception e)
        {
            //Error Handling
            return new EmployeeResponse($"An error occurred while saving the employee: {e:Message}");
        }
    }

    public async Task<EmployeeResponse> UpdateAsync(int employeeId, Employee employee)
    {
        var existingEmployee = await _employeeRepository.FindByIdAsync(employeeId);
        //Validate client
        if (existingEmployee == null)
            return new EmployeeResponse("Employee not found");
        //Validate user
        var existingUser = await _userRepository.FindByIdAsync(employee.UserId);
        if (existingUser == null)
            return new EmployeeResponse("Invalid User");
        
        //Modify Fields
        existingEmployee.DocumentType = employee.DocumentType;
        existingEmployee.NumberIdentification = employee.NumberIdentification;
        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Phone = employee.Phone;
        existingEmployee.Email = employee.Email;
        existingEmployee.City = employee.City;
        existingEmployee.Address=employee.Address;
        existingEmployee.JobPosition = employee.JobPosition;

        try
        {
            _employeeRepository.Update(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return new EmployeeResponse(existingEmployee);
        }
        catch (Exception e)
        {
            //Error Handling
            return new EmployeeResponse($"An error occurred while updating the employee: {e.Message}");
        }
    }

    public async Task<EmployeeResponse> DeleteAsync(int employeeId)
    {
        var existingEmployee = await _employeeRepository.FindByIdAsync(employeeId);

        if (existingEmployee == null)
            return new EmployeeResponse("Employee not found");

        try
        {
            _employeeRepository.Remove(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return new EmployeeResponse(existingEmployee);
        }
        catch (Exception e)
        {
            return new EmployeeResponse($"An error occurred while deleting the employee: {e.Message}");
        }
    }
}