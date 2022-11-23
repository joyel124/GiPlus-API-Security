using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Resources;
using GiPlus.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService employeeService, IMapper mapper){
        _employeeService=employeeService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResource>),statusCode:200)]
    public async Task<IEnumerable<EmployeeResource>> GetAllAsync(){
        var employee=await _employeeService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employee);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The employee was successfully created", typeof(EmployeeResource))]
    [SwaggerResponse(400, "The employee data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);

        var result = await _employeeService.SaveAsync(employee);

        if (!result.Success)
            return BadRequest(result.Message);

        var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);

        return Ok(employeeResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EmployeeResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The employee was successfully updated", typeof(EmployeeResource))]
    [SwaggerResponse(400, "The employee data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);

        var result = await _employeeService.UpdateAsync(id, employee);

        if (!result.Success)
            return BadRequest(result.Message);

        var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);

        return Ok(employeeResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(EmployeeResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The employee was successfully deleted", typeof(EmployeeResource))]
    [SwaggerResponse(400, "The employee data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _employeeService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);

        return Ok(employeeResource);
    }
}