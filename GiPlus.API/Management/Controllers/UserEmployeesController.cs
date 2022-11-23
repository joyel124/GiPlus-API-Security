using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/employees")]
[Produces(MediaTypeNames.Application.Json)]
public class UserEmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public UserEmployeesController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Employees for given User",
        Description = "Get existing employees associated with the specified User",
        OperationId = "GetUserEmployees",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<EmployeeResource>> GetAllByUserIdAsync(int userId)
    {
        var employees = await _employeeService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);

        return resources;
    }
}