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
[SwaggerTag("Create, read, update and delete clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientsController(IClientService clientService, IMapper mapper){
        _clientService=clientService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientResource>),statusCode:200)]
    public async Task<IEnumerable<ClientResource>> GetAllAsync(){
        var contact=await _clientService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(contact);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClientResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The client was successfully created", typeof(ClientResource))]
    [SwaggerResponse(400, "The client data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveClientResource, Client>(resource);

        var result = await _clientService.SaveAsync(client);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Ok(clientResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ClientResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The client was successfully updated", typeof(ClientResource))]
    [SwaggerResponse(400, "The client data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveClientResource, Client>(resource);

        var result = await _clientService.UpdateAsync(id, client);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Ok(clientResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ClientResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The client was successfully deleted", typeof(ClientResource))]
    [SwaggerResponse(400, "The client data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _clientService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Ok(clientResource);
    }
}