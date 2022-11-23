using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services.Communication;

public class EmployeeResponse : BaseResponse<Employee>
{
    protected EmployeeResponse(string message) : base(message)
    {
    }

    protected EmployeeResponse(Employee resource) : base(resource)
    {
    }
}