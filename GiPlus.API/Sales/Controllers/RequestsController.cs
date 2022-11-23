using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Resources;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Resources;
using GiPlus.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete requests")]
public class RequestsController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public RequestsController(IRequestService requestService, IMapper mapper){
        _requestService=requestService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RequestResource>),statusCode:200)]
    public async Task<IEnumerable<RequestResource>> GetAllAsync(){
        var request=await _requestService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(request);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RequestResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The request was successfully created", typeof(RequestResource))]
    [SwaggerResponse(400, "The request data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveRequestResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var request = _mapper.Map<SaveRequestResource, Request>(resource);

        var result = await _requestService.SaveAsync(request);

        if (!result.Success)
            return BadRequest(result.Message);

        var requestResource = _mapper.Map<Request, RequestResource>(result.Resource);

        return Ok(requestResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RequestResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The request was successfully updated", typeof(RequestResource))]
    [SwaggerResponse(400, "The request data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRequestResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var request = _mapper.Map<SaveRequestResource, Request>(resource);

        var result = await _requestService.UpdateAsync(id, request);

        if (!result.Success)
            return BadRequest(result.Message);

        var requestResource = _mapper.Map<Request, RequestResource>(result.Resource);

        return Ok(requestResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(RequestResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The request was successfully deleted", typeof(RequestResource))]
    [SwaggerResponse(400, "The request data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _requestService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var requestResource = _mapper.Map<Request, RequestResource>(result.Resource);

        return Ok(requestResource);
    }
}